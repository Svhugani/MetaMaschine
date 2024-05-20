using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour, IInputManager
{
    public Vector3 ViewPosition {get; private set;}

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
    public event Action OnMPMlick;
    public event Action OnPointerMove;
    public event Action OnRPMClick;

    private void Update()
    {
        ScreenPosition = Input.mousePosition;

        if(ScreenPosition != _prevScreenPosition)
        {
            TriggerOnPointerMove();
        }

        _prevScreenPosition = ScreenPosition;

        ViewPosition = new Vector3(
            2 * (ScreenPosition.x / Screen.width - 0.5f), 
            2 * (ScreenPosition.y / Screen.height - 0.5f), 
            ScreenPosition.z
            );

        PointerDelta = Input.mousePositionDelta;
        Scroll = Input.mouseScrollDelta;

        if(Input.GetMouseButtonDown(0)) 
        {
            LPMPressed = true;
            LPMReleased = false;
            LPMHolded = false;
        }

        else if(Input.GetMouseButtonUp(0))
        {
            if(LPMHolded || LPMPressed) TriggerOnLPMClick();

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
        OnMPMlick?.Invoke();
    }

    public void TriggerOnPointerMove()
    {
        OnPointerMove?.Invoke();    
    }
}
