using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StackableBase : MonoBehaviour, IStackable
{
    public IStacker Stacker { get; set; }

    public bool IsStacked { get { return Stacker == null ? false : true; } }

    public void OnStacked(IStacker stacker)
    {
        Stacker = stacker;
    }

    public void OnUnstacked()
    {
        transform.SetParent(null);
    }
}
