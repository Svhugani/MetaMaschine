
using System;
using UnityEngine;

public interface IInputManager
{
    public Vector3 ViewPosition { get;}
    public Vector3 ScreenPosition { get;}
    public Vector3 PointerDelta { get; }
    public Vector2 Scroll { get; }
    public bool LPMPressed { get; }
    public bool LPMReleased { get; }    
    public bool LPMHolded { get; }
    public bool RPMPressed { get; }
    public bool RPMReleased { get; }
    public bool RPMHolded { get; }
    public bool MPMPressed { get; }
    public bool MPMReleased { get; }
    public bool MPMHolded { get; }
    public event Action OnLPMClick;
    public event Action OnPointerMove;
    public void TriggerOnLPMClick();
    public void TriggerOnPointerMove();

}
