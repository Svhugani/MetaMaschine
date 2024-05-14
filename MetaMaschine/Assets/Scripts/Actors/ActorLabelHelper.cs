using NUnit.Framework;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ActorLabelHelper : MonoBehaviour
{
    public Actor Actor { get; set; }

    private IViewManager _viewManager;

    [Inject] public void Construct(IViewManager viewManager)
    {
        _viewManager = viewManager;
    }

    public void SetTextValue(string text)
    {
        Actor.Label.text = text;    
    }

    private void Update()
    {
        Actor.LabelContainer.forward = - _viewManager.GetCurrentCamera().transform.forward;
    }
}
