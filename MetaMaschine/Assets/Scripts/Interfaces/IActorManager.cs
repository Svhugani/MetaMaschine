
using NUnit.Framework;
using System.Collections.Generic;

public interface IActorManager : IManager
{
    public List<IActor> Actors { get; }
/*    public void SetWarningInfo(string actorID);
    public void SetErrorInfo(string actorID);
    public void SetDefaultInfo(string actorID);
    public void SetWarningInfo(IActor actor);
    public void SetErrorInfo(IActor actor);
    public void SetDefaultInfo(IActor actor);
    public void SetMaintenanceInfo(IActor actor);   */
    public void RaycastToSelection();
    public void RaycastToHover();
    public bool GetActor(string actorID, out IActor actor);
    public bool GetActor(int pointer, out IActor actor);    
    public int GetActorCount(); 
    public void HandleSelection(IActor actor);
    public void HandleHover(IActor actor);
}
