
using UnityEngine;

public interface IActor 
{
    public IActorSuperState CurrentSuperState { get; }
    public IActorSubState CurrentSubState { get; }

    public Vector3 GetPosition();
    public Vector3 GetRotation();
    public Vector3 GetScale();
    public void SetPosition(Vector3 position);
    public void SetRotation(Vector3 rotation);
    public void SetScale(Vector3 scale);
    public void ChangeSuperState(IActorSuperState newState);


}
