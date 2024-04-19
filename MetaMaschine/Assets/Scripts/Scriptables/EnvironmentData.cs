using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "EnvData", menuName = "Scriptables/EnvData")]
public class EnvironmentData : ScriptableObject
{
    [field: SerializeField, Header("ENVIRONMENT SETTINGS")] public float Value { get; private set; }

    private readonly List<IActor> _actors = new();

    public void RegisterActor(IActor actor)
    {
        _actors.Add(actor);
    }

    public IReadOnlyList<IActor> GetActors()
    { 
        return _actors.AsReadOnly();
    }
}
