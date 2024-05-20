using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUIManager : IManager
{
    public void SetActorsFactoryVisibility(bool enable);
    public void SetupActorsFactoryPanel(string factoryName, string floorName, List<IActor> actors);
}
