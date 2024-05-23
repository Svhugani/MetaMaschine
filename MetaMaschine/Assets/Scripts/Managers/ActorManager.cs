using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorManager : MonoBehaviour, IActorManager
{
    [SerializeField] private ActorMarker actorMarker;
    private IInputManager _inputManager;
    private IViewManager _viewManager;
    private IVisualManager _visualManager;
    private IUIManager _uiManager;
    public List<IActor> Actors { get; private set; } = new List<IActor>();

    private IActor _selected;
    private IActor _hovered;

    public event Action<IActor> OnSelectionChange;

    [Inject] public void Construct(IInputManager inputManger, IViewManager viewManager, IVisualManager visualManager, IUIManager uiManager)
    {
        _inputManager = inputManger;
        _viewManager = viewManager;
        _visualManager = visualManager;
        _uiManager = uiManager;

        _inputManager.OnLPMClick += RaycastToSelection;
        _inputManager.OnPointerMove += RaycastToHover;
        _inputManager.OnRPMClick += ClearSelection;
    }

    private void Start()
    {
        Actor[] temp = FindObjectsByType<Actor>(FindObjectsSortMode.InstanceID);

        Actors.Clear();
        
        foreach(var a in temp)
        {
            Actors.Add(a);
        }

        foreach(IActor a in Actors)
        {
            Actor actor = a as Actor;   
            GameObject initModel = actor.DefaultModelContainer.GetChild(0).gameObject;
            actor.SetDefaultModel(initModel);
            

            ActorData data = new ActorData();   
            data.ActorName = initModel.name;

            actor.TriggerOnDataSet(data);
            actor.TriggerOnDynamicDatSet(null);
            Destroy(initModel);

        }

        _uiManager.SetupActorsFactoryPanel("AVIO CHEM LAB FACILITY", "LAB E-24", Actors);
    }


    public void RaycastToSelection()
    {
        if (RaycastActors(out IActor result))
        {
            HandleSelection(result); 
        }
    }

    public void RaycastToHover()
    {
        if (_selected != null) return;
        if (RaycastActors(out IActor result))
        {
            HandleHover(result);
        }
        else if (_hovered != null)
        {
            _hovered.TriggerOnDefault();
            _hovered = null;
            actorMarker.Unmark();
        }
    }

    public void HandleSelection(IActor actor)
    {
        if (_hovered == actor) _hovered = null;
        if (_selected != actor)
        {
            if (_selected != null) _selected.TriggerOnDefault();
            actor.TriggerOnSelected();
            _selected = actor;

            actorMarker.Mark(_selected);
            TriggerOnSelectionChange();
        }
    }

    public void HandleHover(IActor actor)
    {
        if (_selected != actor && _hovered != actor)
        {
            if (_hovered != null) _hovered.TriggerOnDefault();
            actor.TriggerOnHovered();
            _hovered = actor;

            actorMarker.Mark(_hovered);
        }
    }

    public void ClearSelection()
    {
        if (_selected != null)
        {
            _selected.TriggerOnDefault();
            _selected = null;
            actorMarker.Unmark();
            TriggerOnSelectionChange();
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
        foreach(var actor in Actors)
        {
            actor.TriggerOnDefault();  
        }
    }

    public bool GetActor(string actorID, out IActor actor)
    {
        actor = null;   

        foreach(var a in Actors)
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

        if(pointer < Actors.Count )
        {
            actor = Actors[pointer];
            return true;
        }

        return false;
    }

    public int GetActorCount()
    {
        return Actors.Count;
    }

    public void TriggerOnSelectionChange()
    {
        OnSelectionChange?.Invoke(_selected);
    }
}
