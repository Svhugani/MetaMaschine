using UnityEngine;
using Zenject;

public class DataflowManager : IDataFlowManager
{
    IActorManager _actorManager;

    [Inject] public void Construct(IActorManager actorManager)
    {
        _actorManager = actorManager;
    }

    public void PassDataToAgents()
    {
        throw new System.NotImplementedException();
    }
}
