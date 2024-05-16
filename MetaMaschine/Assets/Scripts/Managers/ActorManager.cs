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
    private IVisualManager _visualManager;
    private Actor[] _actors;

    private IActor _selected;
    private IActor _hovered;

    [Inject] public void Construct(IInputManager inputManger, IViewManager viewManager, IVisualManager visualManager)
    {
        _inputManager = inputManger;
        _viewManager = viewManager;
        _visualManager = visualManager;

        _inputManager.OnLPMClick += RaycastToSelection;
        _inputManager.OnPointerMove += RaycastToHover;
    }

    private void Start()
    {
        _actors = FindObjectsByType<Actor>(FindObjectsSortMode.InstanceID);

        foreach(IActor a in _actors)
        {
            Actor actor = a as Actor;   
            GameObject initModel = actor.DefaultModelContainer.GetChild(0).gameObject;
            actor.SetDefaultModel(initModel);
            

            ActorData data = new ActorData();   
            data.ActorName = initModel.name;
            actor.ActorData = data;

            Destroy(initModel);

            SetDefaultInfo(actor);
        }
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

    public void RaycastToHover()
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

    public void SetWarningInfo(string actorID)
    {
        if(GetActor(actorID, out IActor actor))
        {
            SetWarningInfo(actor);
        }
    }

    public void SetErrorInfo(string actorID)
    {
        if (GetActor(actorID, out IActor actor))
        {
            SetErrorInfo(actor);
        }
    }

    public void SetDefaultInfo(string actorID)
    {
        if (GetActor(actorID, out IActor actor))
        {
            SetDefaultInfo(actor);
        }
    }

    public void SetWarningInfo(IActor actor)
    {
        actor.SetInfoVisibility(true);
        actor.SetLabelValue("WARNING");
        actor.SetIcon(_visualManager.GetWarningIcon());
        actor.SetInfoPriority(InfoPriority.Medium);
    }

    public void SetErrorInfo(IActor actor)
    {
        actor.SetInfoVisibility(true);
        actor.SetLabelValue("ERROR");
        actor.SetIcon(_visualManager.GetErrorIcon());
        actor.SetInfoPriority(InfoPriority.High);
    }

    public void SetDefaultInfo(IActor actor)
    {
        actor.SetInfoVisibility(true);
        actor.SetIconVisibility(false);
        actor.SetLabelValue(actor.ActorData.ActorName);
        actor.SetInfoPriority(InfoPriority.Low);
    }

    public bool GetActor(string actorID, out IActor actor)
    {
        actor = null;   

        foreach(var a in _actors)
        {
            if(a.ActorData.ActorID == actorID)
            {
                actor = a;
                return true;
            }
        }

        return false;
    }

    public bool GetActor(int pointer, out IActor actor)
    {
        actor = null;

        if(pointer < _actors.Length )
        {
            actor = _actors[pointer];
            return true;
        }

        return false;
    }

    public int GetActorCount()
    {
        return _actors.Length;
    }
}
