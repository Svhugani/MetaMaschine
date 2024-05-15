using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using Zenject;

public class ActorManager : MonoBehaviour, IActorManager
{
    [SerializeField] private ActorMarker actorMarker;
    private IInputManager _inputManager;
    private IViewManager _viewManager;
    private Actor[] _actors;

    private IActor _selected;
    private IActor _hovered;

    [Inject] public void Construct(IInputManager inputManger, IViewManager viewManager)
    {
        _inputManager = inputManger;
        _viewManager = viewManager;

        _inputManager.OnLPMClick += RaycastToSelection;
        _inputManager.OnPointerMove += RaycastToHovered;
    }

    private void Start()
    {
        _actors = FindObjectsByType<Actor>(FindObjectsSortMode.InstanceID);
    }


    public void RaycastToSelection()
    {

        if (RaycastActors(out IActor result))
        {
            if(_hovered == result) _hovered = null;
            if (_selected != result)
            {
                if(_selected != null) _selected.EnterDefaultState();
                result.EnterSelectState();
                _selected = result;

                actorMarker.Mark(_selected);
            }    
        }

        else if (_selected != null) 
        {
            _selected.EnterDefaultState();
            _selected = null;
            actorMarker.Unmark();
        }
        
    }

    public void RaycastToHovered()
    {
        if (_selected != null) return;
        if (RaycastActors(out IActor result)) 
        {
            if(_selected != result && _hovered != result)
            {
                if (_hovered != null) _hovered.EnterDefaultState();
                result.EnterHoverState();
                _hovered = result;

                actorMarker.Mark(_hovered);
            }
        }
        else if (_hovered != null)
        {
            _hovered.EnterDefaultState();
            _hovered = null;
            actorMarker.Unmark();
        }
    }

    public bool RaycastActors(out IActor result)
    {
        Ray ray = _viewManager.GetCurrentCamera().ScreenPointToRay(_inputManager.ScreenPosition);
        RaycastHit hit;

        result = null;

        if(Physics.Raycast(ray, out hit))
        {
            ActorCollider actorCollider = hit.collider.GetComponent<ActorCollider>();
            if (actorCollider != null)
            {
                result = actorCollider.Parent;
                return true;
            }
        }

        return false;
    }

    public void DeselectAllActors()
    {
        foreach(var actor in _actors)
        {
            actor.EnterDefaultState();  
        }
    }
}
