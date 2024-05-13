using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorManager : MonoBehaviour, IActorManager
{
    private IInputManager _inputManager;
    private IViewManager _viewManager;
    private Actor[] _actors;
    [Inject] public void Construct(IInputManager inputManger, IViewManager viewManager)
    {
        _inputManager = inputManger;
        _viewManager = viewManager;

        _inputManager.OnLPMClick += RaycastActors;
    }

    private void Start()
    {
        _actors = FindObjectsByType<Actor>(FindObjectsSortMode.InstanceID);
    }

    public void RaycastActors()
    {
        Ray ray = _viewManager.GetCurrentCamera().ScreenPointToRay(_inputManager.ScreenPosition);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit))
        {
            ActorCollider actorCollider = hit.collider.GetComponent<ActorCollider>();
            if (actorCollider != null)
            {
                actorCollider.Parent.EnterSelectState();
            }

            else DeselectAllActors();
        }

        else DeselectAllActors();
    }

    public void DeselectAllActors()
    {
        foreach(var actor in _actors)
        {
            actor.EnterDefaultState();  
        }
    }
}
