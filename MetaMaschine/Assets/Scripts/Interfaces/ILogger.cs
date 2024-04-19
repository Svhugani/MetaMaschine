using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ILogger 
{
    public void LogMessage(string message, LogChannel channel);
    public void LogWarning(string message, LogChannel channel);
    public void LogError(string message, LogChannel channel);   
}
