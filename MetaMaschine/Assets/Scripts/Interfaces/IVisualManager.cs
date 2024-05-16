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
    public Material GetDefaultMaterial();
    public Material GetSelectMaterial();
    public Material GetHoverMaterial();
    public Material GetMarkerHoverMaterial();   
    public Material GetMarkerSelectMaterial();


}
