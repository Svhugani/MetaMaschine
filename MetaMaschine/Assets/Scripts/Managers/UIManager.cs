using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class UIManager : MonoBehaviour, IUIManager
{
    [SerializeField] private ActorsFactoryPanelUI actorsFactoryPanelUI;
    [SerializeField] private ActorsDataPanelUI actorsDataPanelUI;



    public void SetActorsFactoryVisibility(bool visibility)
    {
        actorsFactoryPanelUI.SetVisibility(visibility);
    }

    public void SetupActorsFactoryPanel(string factoryName, string floorName, List<IActor> actors)
    {
        actorsFactoryPanelUI.SetData("AVIO CHEM LAB FACILITY", "LAB E-24", actors);
    }
}
