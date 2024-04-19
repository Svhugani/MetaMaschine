using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actor : MonoBehaviour, IActor
{
    public IActorSuperState CurrentSuperState { get; private set; }
    public IActorSubState CurrentSubState { get; private set; }

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
        return Vector3.zero;
    }

    public Vector3 GetRotation()
    {
        return Vector3.zero;
    }

    public Vector3 GetScale()
    {
        return Vector3.one;
    }

    public void SetPosition(Vector3 position)
    {

    }

    public void SetRotation(Vector3 rotation)
    {

    }

    public void SetScale(Vector3 scale)
    {

    }
}
