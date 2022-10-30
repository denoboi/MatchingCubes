using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable : IComponent, IObstacleTarget
{
    public StackTypeSO StackType { get; set; }
    public IStacker Stacker { get; }

    public bool IsStacked { get; }

    public void OnStacked(IStacker stacker);
    public void OnUnstacked();
    public void Dispose();
}
