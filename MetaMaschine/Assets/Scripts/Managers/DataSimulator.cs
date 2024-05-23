using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class DataSimulator : MonoBehaviour
{
    [SerializeField] private AnimationCurve curveA = AnimationCurve.EaseInOut(0, 0, 1, 0);
    [SerializeField] private AnimationCurve curveB = AnimationCurve.EaseInOut(0, 0, 1, 0);
    [SerializeField] private AnimationCurve curveC = AnimationCurve.EaseInOut(0, 0, 1, 0);
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color warningColor = Color.white;
    [SerializeField] private Color errorColor = Color.white;
    [SerializeField] private Color maintenanceColor = Color.white;

    private IActorManager _actorManager;
    private float _timer;
    private float _fetchTimer;
    private const float DATA_TIME_STEP = 5f;
    private const float TIME_SERIES_DURATION = 20f;
    [Inject] public void Construct(IActorManager actorManager)
    {
        _actorManager = actorManager;   
    }

    private void Update()
    {

        if (_actorManager == null) return;

        if (_fetchTimer >= DATA_TIME_STEP)
        {
            UpdateActorDynamicData(_timer / TIME_SERIES_DURATION);
            _fetchTimer = 0;
        }

        if (_timer >= TIME_SERIES_DURATION)
        {
            _timer = 0;
        }

        _timer += Time.deltaTime;
        _fetchTimer += Time.deltaTime;
    }


    private void UpdateActorDynamicData(float normTime)
    {
        if (_actorManager == null) return;

        KPI performance = new KPI("PERFORMANCE", 100 * curveA.Evaluate(normTime), "%", 0, 0, 100);
        KPI ooe = new KPI("OOE", curveB.Evaluate(normTime), "", 2, 0, 1);
        KPI odf = new KPI("ODF", 0.5f + 0.5f * curveB.Evaluate(1 - normTime), "", 2, 0, 1);
        KPI temperature = new KPI("TEMPERATURE", 100 * (0.2f + 0.5f * curveC.Evaluate(1 - normTime)), "°C", 0, 0, 100);
        KPI kvp = new KPI("KVP", 100 * (0.4f + 0.6f * curveC.Evaluate(normTime)), "%", 0, 0, 100);
        KPI duration = new KPI("DURATION", 20 * normTime, "h", 0, null, null);

        ActorDynamicData dynamicData = new ActorDynamicData();

        List<KPI> kpis = new List<KPI>() { performance, ooe, odf, temperature, kvp, duration };
        dynamicData.KPIs = kpis;



        foreach (IActor a in _actorManager.Actors)
        {

            float r = Random.Range(0.0f, 1.0f);
            if (r < 0.3f)
            {
                r = Random.Range(0.0f, 1.0f);

                if (r < .08f)
                {
                    dynamicData.ActorStatus = GetErrorStatus();
                }

                else if (r < .20f)
                {
                    dynamicData.ActorStatus = GetWarnStatus();
                }

                else if (r < .25f)
                {
                    dynamicData.ActorStatus = GetMaintenanceStatus();
                }

                else
                {
                    dynamicData.ActorStatus = GetNormalStatus();
                }
            }

            a.TriggerOnDynamicDatSet(dynamicData);
        }
    }


    private ActorStatus GetNormalStatus()
    {
        return new ActorStatus("0", "", "", "", 0, normalColor);
    }

    private ActorStatus GetWarnStatus()
    {
        return new ActorStatus("1", "WARNING", "A warning state.", "1", 10, warningColor);
    }

    private ActorStatus GetErrorStatus()
    {
        return new ActorStatus("2", "ERROR", "An error state.", "2", 20, errorColor);
    }

    private ActorStatus GetMaintenanceStatus()
    {
        return new ActorStatus("3", "MAINTENANCE", "A maintenance state.", "3", 5, maintenanceColor);
    }
}
