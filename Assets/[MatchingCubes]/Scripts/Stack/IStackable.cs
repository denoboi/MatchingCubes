using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable : IComponent, IObstacleTarget
{
    public StackTypeSO StackType { get; set; }
    public IStacker Stacker { get; }

    public bool IsStacked { get; }
    public bool CanStack { get; }

    public void Initialise();
    public void UpdateStackType(StackTypeSO type);

    public void OnStacked(IStacker stacker);
    public void OnUnstacked();
    public void Dispose();
}
