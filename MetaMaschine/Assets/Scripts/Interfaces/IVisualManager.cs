using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IVisualManager : IManager
{
    public GameObject GetDefaultModel(string modelID);
    public GameObject GetSymbolicModel(string modelID);
    public Texture2D GetIcon(string iconID);
    public Texture2D GetWarningIcon();
    public Texture2D GetErrorIcon();
    public Texture2D GetMaintenanceIcon();
    public Material GetDefaultMaterial();
    public Material GetSelectMaterial();
    public Material GetHoverMaterial();
    public Material GetMarkerHoverMaterial();   
    public Material GetMarkerSelectMaterial();
    public void SetActorMaterialToDefault();
    public void SetActorMaterialToXRay();
    public Color GetErrorColor();
    public Color GetWarningColor();
    public Color GetDefaultColor();
    public Color GetMaintenanceColor();

}
