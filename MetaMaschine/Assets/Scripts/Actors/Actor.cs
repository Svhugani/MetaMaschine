using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

[RequireComponent(typeof(ActorModelHelper), typeof(ActorLabelHelper), typeof(ActorAnimationHelper))]
public class Actor : MonoBehaviour, IActor
{
    [field: SerializeField] public Transform DefaultModelContainer { get; private set; }
    [field: SerializeField] public Transform SymbolicModelContainer { get; private set; }
    [field: SerializeField] public Transform LabelContainer { get; private set; }
    public IActorSuperState CurrentSuperState { get; private set; }
    public IActorSubState CurrentSubState { get; private set; }
    public ActorData ActorData { get; set; }
    private ActorAnimationHelper _animationHelper;
    private ActorModelHelper _modelHelper;
    private ActorLabelHelper _labelHelper;
    private IVisualManager _visualManager;

    private void Awake()
    {
        _animationHelper = GetComponent<ActorAnimationHelper>();
        _modelHelper = GetComponent<ActorModelHelper>();
        _labelHelper = GetComponent<ActorLabelHelper>();

        _modelHelper.Actor = this;
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
        _modelHelper.SetModel(_visualManager.GetDefaultModel(modelID));
    }

    public void SetLabelValue(string value)
    {

    }

    public void SetLabelVisibility(bool visibility)
    {

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
        _modelHelper.SetMaterial(_visualManager.GetDefaultMaterial());
    }

    public void EnterHoverState()
    {

    }

    public void EnterSelectState()
    {
        Debug.Log($"Entity {transform.name} selected");
        _modelHelper.SetMaterial(_visualManager.GetSelectMaterial());
    }

    public void EnterInvisibleState()
    {

    }
}
