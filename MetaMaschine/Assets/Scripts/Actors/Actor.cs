using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ActorModelHelper), typeof(ActorInfoHelper), typeof(ActorAnimationHelper))]
public class Actor : MonoBehaviour, IActor
{
    [field: SerializeField] public Transform DefaultModelContainer { get; private set; }
    [field: SerializeField] public Transform SymbolicModelContainer { get; private set; }
    [field: SerializeField] public Transform LabelContainer { get; private set; }
    
    public IActorSuperState CurrentSuperState { get; private set; }
    public IActorSubState CurrentSubState { get; private set; }
    public ActorData ActorData { get; private set; }
    public ActorDynamicData ActorDynamicData { get; private set; }
    public ActorState ActorState { get; private set; }


    private ActorAnimationHelper _animationHelper;
    private ActorModelHelper _modelHelper;
    private ActorInfoHelper _infoHelper;
    private IVisualManager _visualManager;

    public event Action<IActor> OnSelected;
    public event Action<IActor> OnHovered;
    public event Action<IActor> OnDefault;
    public event Action<IActor> OnInvisible;
    public event Action<IActor> OnActorDataSet;
    public event Action<IActor> OnActorDynamicDataSet;

    private void Awake()
    {
        _animationHelper = GetComponent<ActorAnimationHelper>();
        _modelHelper = GetComponent<ActorModelHelper>();
        _infoHelper = GetComponent<ActorInfoHelper>();

        _modelHelper.Actor = this;
        _infoHelper.Actor = this;
    }

    [Inject]
    public void Construct(IVisualManager visualManager)
    {
        _visualManager = visualManager;
    }

    public void ChangeSuperState(IActorSuperState newState)
    {
        if (CurrentSuperState != null)
        {
            CurrentSuperState.ExitSuperState(this);
        }

        CurrentSuperState = newState;
        CurrentSuperState.EnterSuperState(this);
    }

    public Transform GetTransform()
    {
        return DefaultModelContainer;
    }

    public Vector3 GetPosition()
    {
        return ActorData.Position;  
    }

    public Vector3 GetRotation()
    {
        return ActorData.Rotation;
    }

    public Vector3 GetScale()
    {
        return ActorData.Scale;
    }

    public Bounds GetBounds()
    {
        return _modelHelper.Bounds;
    }

    public void SetActorCategory(string category)
    {
        ActorData.ActorCategory = category;
    }

    public void SetActorDescription(string actorDescription)
    {
        ActorData.ActorDescription = actorDescription;
    }

    public void SetActorName(string actorName)
    {
        ActorData.ActorName = actorName;
    }

    public void SetActorParent(string parentID)
    {
        ActorData.ParentID = parentID;  
    }

    public void SetActorType(ActorType actorType)
    {
        ActorData.ActorType = actorType;
    }

    public void SetDefaultModel(string modelID)
    {
        _modelHelper.SetModel(modelID);
    }

    public void SetDefaultModel(GameObject newModel)
    {
        _modelHelper.SetModel(newModel);
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;  
        ActorData.Position = position;
    }

    public void SetRotation(Vector3 rotation)
    {
        DefaultModelContainer.localEulerAngles = rotation;
        ActorData.Rotation = rotation;
    }

    public void SetScale(Vector3 scale)
    {
        DefaultModelContainer.localScale = scale;
        ActorData.Scale = scale;
    }

    public void SetSymbolicModel(string modelID)
    {

    }

    public void EnterDefaultState()
    {
        ActorState = ActorState.Default;
        _modelHelper.SetMaterial(_visualManager.GetDefaultMaterial());
    }

    public void EnterHoverState()
    {
        ActorState = ActorState.Hovered;
        _modelHelper.SetMaterial(_visualManager.GetHoverMaterial());
    }

    public void EnterSelectState()
    {
        ActorState = ActorState.Selected;
        _modelHelper.SetMaterial(_visualManager.GetSelectMaterial());
    }

    public void EnterInvisibleState()
    {
        ActorState = ActorState.Invisible;
    }

    public void SetInfoVisibility(bool visibility)
    {
        LabelContainer.gameObject.SetActive(visibility);
    }

    public void SetInfoPriority(int priority)
    {
        _infoHelper.SetInfoPriority(priority);
    }

    public void SetInfoColor(Color color)
    {
        _infoHelper.SetInfoColor(color);
    }

    public void SetIconVisibility(bool visibility)
    {
        _infoHelper.SetIconVisibility(visibility);
    }

    public void SetIcon(string iconID)
    {
        _infoHelper.SetIcon(iconID);
    }

    public void SetIcon(Texture2D icon)
    {
        _infoHelper.SetIcon(icon);
    }
    public void SetLabelValue(string value)
    {
        _infoHelper.SetTextValue(value);
    }

    public void SetLabelVisibility(bool visibility)
    {
        _infoHelper.SetLabelVisibility(visibility);
    }

    public void TriggerOnSelected()
    {
        EnterSelectState();
        OnSelected?.Invoke(this);
    }

    public void TriggerOnHovered()
    {
        EnterHoverState();
        OnHovered?.Invoke(this);
    }

    public void TriggerOnDefault()
    {
        EnterDefaultState();
        OnDefault?.Invoke(this);
    }

    public void TriggerOnInvisible()
    {
        EnterInvisibleState();
        OnInvisible?.Invoke(this);
    }

    public void TriggerOnDataSet(ActorData data)
    {
        ActorData = data;
        OnActorDataSet?.Invoke(this);
    }

    public void TriggerOnDynamicDatSet(ActorDynamicData dynamicData)
    {
        ActorDynamicData = dynamicData;
        OnActorDynamicDataSet?.Invoke(this);
        Debug.Log("Trigger on dynamic data set");

        Texture2D texture = null;
        Color color = _visualManager.GetDefaultColor();
        string label;
        int priority = 0;

        if (ActorDynamicData == null || ActorDynamicData.ActorStatus == null || ActorDynamicData.ActorStatus.StatusID == "0")
        {
            label = ActorData.ActorName;
        }

        else
        {
            texture = _visualManager.GetIcon(ActorDynamicData.ActorStatus.IconID);
            color = ActorDynamicData.ActorStatus.Color;
            label = ActorDynamicData.ActorStatus.StatusName;
            priority= ActorDynamicData.ActorStatus.Priority;    
        }


        SetInfoVisibility(true);
        SetIcon(texture);
        SetInfoPriority(priority);
        SetInfoColor(color);
        SetLabelValue(label);
        
    }
}
