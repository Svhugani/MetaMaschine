
using NUnit.Framework;
using System;
using System.Collections.Generic;

public interface IActorManager : IManager
{
    public List<IActor> Actors { get; }
    public void RaycastToSelection();
    public void RaycastToHover();
    public bool GetActor(string actorID, out IActor actor);
    public bool GetActor(int pointer, out IActor actor);    
    public int GetActorCount(); 
    public void HandleSelection(IActor actor);
    public void HandleHover(IActor actor);

    public event Action<IActor> OnSelectionChange;
    public void TriggerOnSelectionChange();
}
