using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ActorsFactoryPanelUI : AbstractCanvasPanel
{
    [SerializeField] private TextMeshProUGUI factoryLabel;
    [SerializeField] private TextMeshProUGUI floorLabel;
    [SerializeField] private ActorButtonUI actorButtonPrefab;
    [SerializeField] private RectTransform actorButtonContainer;

    private IVisualManager _visualManager;
    private IActorManager _actorManager;
    private IViewManager _viewManager;
    private Dictionary<IActor, ActorButtonUI> _actorButtonBindings = new Dictionary<IActor, ActorButtonUI>();

    [Inject]
    public void Construct(IVisualManager visualManager, IActorManager actorManager, IViewManager viewManager)
    {
        _visualManager = visualManager;
        _actorManager = actorManager;
        _viewManager = viewManager; 
    }

    public void SetData(string factoryLabelValue, string floorLabelValue, List<IActor> actors)
    {
        factoryLabel.text = factoryLabelValue;
        floorLabel.text = floorLabelValue;

        // Cleanup old buttons and clear dictionary
        foreach (var kvp in _actorButtonBindings)
        {
            Destroy(kvp.Value.gameObject);
        }
        _actorButtonBindings.Clear();

        if (actors == null || actors.Count == 0) return;

        foreach (IActor a in actors)
        {
            ActorButtonUI newActorButtonUI = Instantiate(actorButtonPrefab, actorButtonContainer);

            a.OnActorDynamicDataSet += UpdateActorUIButton;
            newActorButtonUI.OnButtonClick += ActorButtonAction;
            newActorButtonUI.OnButtonClick += SnapToActor;
            _actorButtonBindings.Add(a, newActorButtonUI);
            UpdateActorUIButton(a); // Initialize the button with current actor data
        }
    }

    private void UpdateActorUIButton(IActor actor)
    {
        Texture2D texture = null;
        Color color = _visualManager.GetDefaultColor();
        string label = "UNKNOWN";

        if (actor.ActorDynamicData != null && actor.ActorDynamicData.ActorStatus != null)
        {
            color = actor.ActorDynamicData.ActorStatus.Color;
            texture = _visualManager.GetIcon(actor.ActorDynamicData.ActorStatus.IconID);
        }

        if (actor.ActorData != null)
        {
            label = actor.ActorData.ActorName;
        }

        if (_actorButtonBindings.TryGetValue(actor, out ActorButtonUI actorButtonUI))
        {
            actorButtonUI.UpdateComponent(label, texture, color, actor);
        }
    }

    private void ActorButtonAction(IActor actor)
    {
        _actorManager.HandleSelection(actor);
    }

    private void SnapToActor(IActor actor)
    {
        _viewManager.MoveAndLookAt(actor.GetTransform(), .3f, 30f, new Vector3(30, 45, 0)); 
    }
}
