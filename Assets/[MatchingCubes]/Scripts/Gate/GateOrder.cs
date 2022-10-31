using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;
using UnityEngine;

public class GateOrder : GateBase
{
    public override void OnInteracted(IStacker stacker)
    {
        List<StackTypeSO> orderedStacks = stacker.Stacks.Select(x => x.StackType).ToList();
        orderedStacks = orderedStacks.OrderBy(s => s.name).ToList();

        for (int i = 0; i < stacker.Stacks.Count; i++)
            stacker.Stacks[i].UpdateStackType(orderedStacks[i]);

        base.OnInteracted(stacker);
    }

}
