using UnityEngine;

public class Logger : MonoBehaviour, ILogger
{
    [SerializeField, Header("GLOBAL")] private bool logMessages = true;
    [SerializeField, Header("PER CHANNEL")] private bool logGeneral = true;
    [SerializeField] private bool logActor = true;
    [SerializeField] private bool logEnvironment = true;
    [SerializeField] private bool logView = true;
    [SerializeField] private bool logVisual = true;
    [SerializeField] private bool logUI = true;
    [SerializeField] private bool logInput = true;

    public void LogError(string message, LogChannel channel)
    {
        if (!logMessages) return;

        switch (channel)
        {
            case LogChannel.General: if (logGeneral) Debug.LogError(message); break;
            case LogChannel.Actor: if (logActor) Debug.LogError(message); break;
            case LogChannel.Environment: if (logEnvironment) Debug.LogError(message); break;
            case LogChannel.View: if (logView) Debug.LogError(message); break;
            case LogChannel.Visual: if (logVisual) Debug.LogError(message); break;
            case LogChannel.UI: if (logUI) Debug.LogError(message); break;
            case LogChannel.Input: if (logInput) Debug.LogError(message); break;
            default: break;
        }
    }

    public void LogMessage(string message, LogChannel channel)
    {
        if (!logMessages) return;

        switch (channel) 
        {
            case LogChannel.General: if(logGeneral) Debug.Log(message); break;
            case LogChannel.Actor: if (logActor) Debug.Log(message); break;
            case LogChannel.Environment: if (logEnvironment) Debug.Log(message); break;
            case LogChannel.View: if (logView) Debug.Log(message); break;
            case LogChannel.Visual: if (logVisual) Debug.Log(message); break;
            case LogChannel.UI: if (logUI) Debug.Log(message); break;
            case LogChannel.Input: if (logInput) Debug.Log(message); break;
            default: break;
        }
    }

    public void LogWarning(string message, LogChannel channel)
    {
        if (!logMessages) return;

        switch (channel)
        {
            case LogChannel.General: if (logGeneral) Debug.LogWarning(message); break;
            case LogChannel.Actor: if (logActor) Debug.LogWarning(message); break;
            case LogChannel.Environment: if (logEnvironment) Debug.LogWarning(message); break;
            case LogChannel.View: if (logView) Debug.LogWarning(message); break;
            case LogChannel.Visual: if (logVisual) Debug.LogWarning(message); break;
            case LogChannel.UI: if (logUI) Debug.LogWarning(message); break;
            case LogChannel.Input: if (logInput) Debug.LogWarning(message); break;
            default: break;
        }
    }

}
