using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ViewManager : MonoBehaviour, IViewManager
{
    [SerializeField] private Camera mainCamera;
    [SerializeField] private float zoomSpeed = 1;
    [SerializeField] private float rotateSpeed = 1;
    [SerializeField] private float rotateDamp = 1;
    [SerializeField] private float dragSpeed = 1;
    [SerializeField] private float dragDamp = 1;

    [Inject] private IInputManager _inputManager;
    private Camera _currentCamera;
    private Vector3 _currentFocusPoint;
    private float _currentDistance;
    private Quaternion _desiredRotation;
    private float _yRotationTarget;
    private float _xRotationTarget;
    private Vector3 _targetShift;

    private void Awake()
    {
        _currentCamera = mainCamera;
        _currentFocusPoint = Vector3.zero;
        _currentDistance = Vector3.Distance(_currentFocusPoint, _currentCamera.transform.position);
        _desiredRotation = _currentCamera.transform.rotation;

    }
    public void BindCameraControlsToType(CameraControlType cameraControlType)
    {

    }

    public void EnableViewType(ViewType viewType)
    {

    }

    public Camera GetCurrentCamera()
    {
        return mainCamera;
    }

    public void ResetView()
    {

    }

    private void Update()
    {
        ControlWithMouse();
        RotateCamera();
        DampenCameraRotation();
        DampenDrag();
    }


    private void RotateCamera()
    {
        if (_currentCamera != null)
        {
            _currentCamera.transform.RotateAround(
                _currentFocusPoint,
                Vector3.up,
                _xRotationTarget
                );

            _currentCamera.transform.RotateAround(
                _currentFocusPoint,
                _currentCamera.transform.right,
                _yRotationTarget
                );

            _currentCamera.transform.position += _targetShift;
            _currentFocusPoint += _targetShift;
        }
    }

    private void DampenCameraRotation()
    {
        _xRotationTarget = Mathf.Lerp(_xRotationTarget, 0, Time.deltaTime * rotateDamp);
        _yRotationTarget = Mathf.Lerp(_yRotationTarget, 0, Time.deltaTime * rotateDamp );
    }

    private void DampenDrag()
    {
        _targetShift = Vector3.Lerp(_targetShift, Vector3.zero, Time.deltaTime * dragDamp);
    }

    public void ControlWithMouse()
    {
        if (_currentCamera == null) return;

        if(_inputManager.LPMHolded)
        {
            _xRotationTarget = _inputManager.PointerDelta.x * rotateSpeed;
            _yRotationTarget = -_inputManager.PointerDelta.y * 0.4f * rotateSpeed;

        }

        else if (_inputManager.RPMHolded) 
        {
            _targetShift = - dragSpeed * _inputManager.PointerDelta.x * _currentCamera.transform.right;
            _targetShift -= dragSpeed * _inputManager.PointerDelta.y * _currentCamera.transform.up;
        }

        _currentCamera.transform.position += _inputManager.Scroll.y *  zoomSpeed * _currentCamera.transform.forward;
    }

}
