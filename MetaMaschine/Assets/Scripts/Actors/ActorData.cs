using UnityEngine;

public class ModelsData
{
    public string DefaultModelID;
    public string SymbolicModelID;
}
public class LabelData
{
    public string value;
    public Color color;
}

public class ActorData 
{
    public string ActorID;
    public string ActorName;
    public string ActorDescription;
    public string ParentID;
    public string ActorCategory;
    public ActorType ActorType;    
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public ModelsData ModelsData;
    public LabelData LabelData;

}
