using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[Serializable]
public class ModelBinding
{
    public string ID;
    public GameObject Model;
}

[Serializable]
public class TextureBinding
{
    public string ID;
    public Texture2D Texture;
}

public class VisualManager : MonoBehaviour, IVisualManager
{
    [SerializeField] private List<ModelBinding> defaultModelBindings;
    [SerializeField] private List<ModelBinding> symbolicModelBindings;
    [SerializeField] private List<TextureBinding> iconBindings;
    [SerializeField] private Texture2D warningIcon;
    [SerializeField] private Texture2D errorIcon;
    [SerializeField] private Material defaultActorMaterial;
    [SerializeField] private Material hoverActorMaterial;
    [SerializeField] private Material selectActorMaterial;
    [SerializeField] private Material markerSelectMaterial;
    [SerializeField] private Material markerHoverMaterial;

    public Material GetDefaultMaterial()
    {
        return defaultActorMaterial;
    }

    public GameObject GetDefaultModel(string modelID)
    {
        foreach (ModelBinding binding in defaultModelBindings) 
        {
            if (binding.ID == modelID) return binding.Model;
        }

        return null;
    }

    public Material GetHoverMaterial()
    {
        return hoverActorMaterial;
    }

    public Material GetMarkerHoverMaterial()
    {
        return markerHoverMaterial;
    }

    public Material GetMarkerSelectMaterial()
    {
        return markerSelectMaterial;
    }

    public Material GetSelectMaterial()
    {
        return selectActorMaterial;
    }

    public GameObject GetSymbolicModel(string modelID)
    {
        foreach (ModelBinding binding in defaultModelBindings)
        {
            if (binding.ID == modelID) return binding.Model;
        }

        return null;
    }

    public Texture2D GetIcon(string iconID)
    {
        foreach (TextureBinding binding in iconBindings)
        {
            if (binding.ID == iconID) return binding.Texture;
        }

        return null;
    }

    public Texture2D GetWarningIcon()
    {
        return warningIcon;
    }

    public Texture2D GetErrorIcon()
    {
        return errorIcon;   
    }
}
