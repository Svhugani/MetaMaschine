
using UnityEngine;

public class ActorIdleSuperState : IActorSuperState
{
    public IActorSubState CurrentSubState { get; private set; }

    public void ChangeSubState(IActorSubState newSubState)
    {

    }

    public void EnterSuperState(IActor actor)
    {

    }

    public void ExitSuperState(IActor actor)
    {

    }

    public void UpdateSuperState(IActor actor)
    {

    }
}
