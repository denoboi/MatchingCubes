using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IStacker 
{
    public List<IStackable> Stacks { get; set; }

    public void RemoveStack(IStackable stack);

    public void AddStack(IStackable stack);
}
