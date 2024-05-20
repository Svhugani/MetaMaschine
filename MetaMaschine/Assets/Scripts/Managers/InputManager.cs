using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager : MonoBehaviour, IInputManager
{
    public Vector3 ViewPosition { get; private set; }
    public Vector3 ScreenPosition { get; private set; }
    public Vector3 PointerDelta { get; private set; }
    public Vector2 Scroll { get; private set; }

    public bool LPMPressed { get; private set; }
    public bool LPMReleased { get; private set; }
    public bool LPMHolded { get; private set; }
    public bool RPMPressed { get; private set; }
    public bool RPMReleased { get; private set; }
    public bool RPMHolded { get; private set; }
    public bool MPMPressed { get; private set; }
    public bool MPMReleased { get; private set; }
    public bool MPMHolded { get; private set; }

    private Vector3 _prevScreenPosition;

    public event Action OnLPMClick;
    public event Action OnMPMClick;
    public event Action OnPointerMove;
    public event Action OnRPMClick;

    private void Update()
    {
        // Check if pointer is over a UI element
        if (EventSystem.current.IsPointerOverGameObject() || IsPointerOverUIElement())
        {
            return;
        }

        // Update mouse position
        ScreenPosition = Input.mousePosition;
        if (ScreenPosition != _prevScreenPosition)
        {
            TriggerOnPointerMove();
        }

        // Convert screen position to view position
        ViewPosition = new Vector3(
            2 * (ScreenPosition.x / Screen.width - 0.5f),
            2 * (ScreenPosition.y / Screen.height - 0.5f),
            ScreenPosition.z
        );

        PointerDelta = ScreenPosition - _prevScreenPosition;
        Scroll = Input.mouseScrollDelta;
        _prevScreenPosition = ScreenPosition;

        // Handle mouse buttons
        HandleMouseButtons();

        // Handle touch inputs
        HandleTouchInputs();
    }

    private void HandleMouseButtons()
    {
        // Left mouse button
        if (Input.GetMouseButtonDown(0))
        {
            LPMPressed = true;
            LPMReleased = false;
            LPMHolded = false;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (LPMHolded || LPMPressed) TriggerOnLPMClick();
            LPMPressed = false;
            LPMReleased = true;
            LPMHolded = false;
        }
        else if (Input.GetMouseButton(0))
        {
            LPMPressed = false;
            LPMReleased = false;
            LPMHolded = true;
        }
        else
        {
            LPMPressed = false;
            LPMReleased = false;
            LPMHolded = false;
        }

        // Right mouse button
        if (Input.GetMouseButtonDown(1))
        {
            RPMPressed = true;
            RPMReleased = false;
            RPMHolded = false;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            if (RPMHolded || RPMPressed) TriggerOnRPMClick();
            RPMPressed = false;
            RPMReleased = true;
            RPMHolded = false;
        }
        else if (Input.GetMouseButton(1))
        {
            RPMPressed = false;
            RPMReleased = false;
            RPMHolded = true;
        }
        else
        {
            RPMPressed = false;
            RPMReleased = false;
            RPMHolded = false;
        }

        // Middle mouse button
        if (Input.GetMouseButtonDown(2))
        {
            MPMPressed = true;
            MPMReleased = false;
            MPMHolded = false;
        }
        else if (Input.GetMouseButtonUp(2))
        {
            if (MPMHolded || MPMPressed) TriggerOnMPMClick();
            MPMPressed = false;
            MPMReleased = true;
            MPMHolded = false;
        }
        else if (Input.GetMouseButton(2))
        {
            MPMPressed = false;
            MPMReleased = false;
            MPMHolded = true;
        }
        else
        {
            MPMPressed = false;
            MPMReleased = false;
            MPMHolded = false;
        }
    }

    private void HandleTouchInputs()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);
            ScreenPosition = touch.position;

            if (touch.phase == TouchPhase.Began)
            {
                LPMPressed = true;
                LPMReleased = false;
                LPMHolded = false;
            }
            else if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)
            {
                if (LPMHolded || LPMPressed) TriggerOnLPMClick();
                LPMPressed = false;
                LPMReleased = true;
                LPMHolded = false;
            }
            else if (touch.phase == TouchPhase.Moved || touch.phase == TouchPhase.Stationary)
            {
                LPMPressed = false;
                LPMReleased = false;
                LPMHolded = true;
            }
            else
            {
                LPMPressed = false;
                LPMReleased = false;
                LPMHolded = false;
            }

            // Check if pointer moved
            if (ScreenPosition != _prevScreenPosition)
            {
                TriggerOnPointerMove();
            }
            _prevScreenPosition = ScreenPosition;

            // Convert screen position to view position
            ViewPosition = new Vector3(
                2 * (ScreenPosition.x / Screen.width - 0.5f),
                2 * (ScreenPosition.y / Screen.height - 0.5f),
                ScreenPosition.z
            );

            PointerDelta = touch.deltaPosition;
        }
    }

    private bool IsPointerOverUIElement()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current)
        {
            position = new Vector2(Input.mousePosition.x, Input.mousePosition.y)
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

    public void TriggerOnLPMClick()
    {
        OnLPMClick?.Invoke();
    }

    public void TriggerOnRPMClick()
    {
        OnRPMClick?.Invoke();
    }

    public void TriggerOnMPMClick()
    {
        OnMPMClick?.Invoke();
    }

    public void TriggerOnPointerMove()
    {
        OnPointerMove?.Invoke();
    }
}
