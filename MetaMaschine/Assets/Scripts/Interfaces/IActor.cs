
using UnityEngine;

public interface IActor 
{
    public ActorData ActorData { get; }
    public IActorSuperState CurrentSuperState { get; }
    public IActorSubState CurrentSubState { get; }
    public void ChangeSuperState(IActorSuperState newState);
    public Vector3 GetPosition();
    public Vector3 GetRotation();
    public Vector3 GetScale();
    public void SetPosition(Vector3 position);
    public void SetRotation(Vector3 rotation);
    public void SetScale(Vector3 scale);
    public void SetLabelVisibility(bool visibility);
    public void SetLabelValue(string value);    
    public void SetDefaultModel(string modelID);
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
}
