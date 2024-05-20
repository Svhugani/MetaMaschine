using UnityEngine;
using Zenject;

public interface IViewManager : IManager
{
    public void ResetView();
    public void BindCameraControlsToType(CameraControlType cameraControlType);
    public void EnableViewType(ViewType viewType);
    public Camera GetCurrentCamera();
    public void MoveAndLookAt(Transform targetTransform, float duration, float distance, Vector3 angles);
}
