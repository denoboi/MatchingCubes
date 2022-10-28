using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStackable : IComponent
{
    public IStacker Stacker { get; }

    public bool IsStacked { get; }

    public void OnStacked(IStacker stacker);
    public void OnUnstacked();
}
