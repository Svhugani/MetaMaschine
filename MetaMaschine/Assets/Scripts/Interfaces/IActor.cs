
using System;
using UnityEngine;

public interface IActor 
{
    public ActorData ActorData { get; }
    public ActorDynamicData ActorDynamicData { get; }
    public ActorState ActorState { get; }
    public IActorSuperState CurrentSuperState { get; }
    public IActorSubState CurrentSubState { get; }
    public void ChangeSuperState(IActorSuperState newState);
    public Vector3 GetPosition();
    public Vector3 GetRotation();
    public Vector3 GetScale();
    public Bounds GetBounds();
    public void SetPosition(Vector3 position);
    public void SetRotation(Vector3 rotation);
    public void SetScale(Vector3 scale);
    public void SetInfoVisibility(bool visibility);    
    public void SetInfoPriority(int priority);
    public void SetInfoColor(Color color);
    public void SetLabelVisibility(bool visibility);
    public void SetLabelValue(string value);
    public void SetIconVisibility(bool visibility);
    public void SetIcon(string iconID);
    public void SetIcon(Texture2D tex);
    public void SetDefaultModel(string modelID);
    public void SetDefaultModel(GameObject newModel);
    public void SetSymbolicModel(string modelID);
    public void SetActorType(ActorType actorType);
    public void SetActorName(string actorName);
    public void SetActorDescription(string actorDescription);
    public void SetActorParent(string parentID);
    public void SetActorCategory(string category);
    public void EnterDefaultState();
    public void EnterHoverState();  
    public void EnterSelectState();
    public void EnterInvisibleState();
    public event Action<IActor> OnSelected;
    public event Action<IActor> OnHovered;
    public event Action<IActor> OnDefault;
    public event Action<IActor> OnInvisible;
    public event Action<IActor> OnActorDataSet;
    public event Action<IActor> OnActorDynamicDataSet;
    public void TriggerOnSelected();
    public void TriggerOnHovered(); 
    public void TriggerOnDefault();
    public void TriggerOnInvisible();
    public void TriggerOnDataSet(ActorData data);
    public void TriggerOnDynamicDatSet(ActorDynamicData dynamicData);


}
