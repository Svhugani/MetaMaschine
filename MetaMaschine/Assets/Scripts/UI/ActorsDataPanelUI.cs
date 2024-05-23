using NUnit.Framework;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Zenject;

public class ActorsDataPanelUI : AbstractCanvasPanel
{
    [SerializeField] private RectTransform dataWidgetContainer;
    [SerializeField] private RadialCirclesWidget circlesWidgetPrefab;
    [SerializeField] private TextMeshProUGUI agentName;

    private List<RadialCirclesWidget> _circlesWidgets = new();
    private IActor _targetActor;

    private IActorManager _actorManager;

    [Inject]
    public void Construct(IActorManager actorManager)
    {
        _actorManager = actorManager;
    }

    private void OnEnable()
    {
        _actorManager.OnSelectionChange += SetTargetActor;
    }

    private void OnDisable()
    {
        _actorManager.OnSelectionChange -= SetTargetActor;
    }

    public void SetTargetActor(IActor actor)
    {
        if(actor == null)
        {
            ClearTargetActor();
            return;
        }

        agentName.text = "DATA: " + actor.ActorData.ActorName;

        UpdateKPIs(actor);
        actor.OnActorDynamicDataSet += UpdateKPIs;
    }

    public void ClearTargetActor()
    {
        agentName.text = "NONE";
        if(_targetActor != null)
        {
            _targetActor.OnActorDynamicDataSet += UpdateKPIs;
        }
        
    }

    public void UpdateKPIs(IActor actor)
    {
        if (actor.ActorDynamicData == null || actor.ActorDynamicData.KPIs == null) return;
        List<KPI> kpis = actor.ActorDynamicData.KPIs;

        for (int i = 0; i < kpis.Count; i++) 
        {
            KPI kpi = kpis[i];  
            if(_circlesWidgets.Count <= i) 
            {
                RadialCirclesWidget widget = Instantiate(circlesWidgetPrefab, dataWidgetContainer);
                _circlesWidgets.Add(widget);
            }
            _circlesWidgets[i].gameObject.SetActive(true);
            _circlesWidgets[i].SetValue(kpi.KpiName, kpi.KpiValue, kpi.MinValue, kpi.MaxValue, kpi.Precision, kpi.Unit);
        }

        for(int i = kpis.Count; i < _circlesWidgets.Count; i++) 
        {
            _circlesWidgets[i].gameObject.SetActive(false);
        }
    }
}
