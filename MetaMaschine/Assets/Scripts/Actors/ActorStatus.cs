using UnityEngine;

public class ActorStatus 
{
    public string StatusID;
    public string StatusName;
    public string StatusDescription;
    public string IconID;
    public int Priority;
    public Color Color;

    public ActorStatus(string statusID, string statusName, string statusDescription, string iconID, int priority, Color color)
    {
        StatusID = statusID;
        StatusName = statusName;
        StatusDescription = statusDescription;
        IconID = iconID;
        Priority = priority;
        Color = color;
    }
}
