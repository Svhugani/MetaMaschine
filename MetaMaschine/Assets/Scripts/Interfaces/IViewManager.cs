using UnityEngine;
using Zenject;

public interface IViewManager : IManager
{
    public void ResetView();
    public void BindCameraControlsToType(CameraControlType cameraControlType);
    public void EnableViewType(ViewType viewType);
    public Camera GetCurrentCamera();
}
