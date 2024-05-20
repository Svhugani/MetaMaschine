
using System.Drawing;

[System.Serializable]
public class KPI 
{
    public string KpiName;
    public float KpiValue;
    public string Unit;
    public int Precision;
    public float? MinValue;
    public float? MaxValue;


    public KPI(string kpiName, float kpiValue, string unit, int precision, float? minValue, float? maxValue)
    {
        KpiName = kpiName;
        KpiValue = kpiValue;
        Unit = unit;
        Precision = precision;
        MinValue = minValue;
        MaxValue = maxValue;
    }
}
