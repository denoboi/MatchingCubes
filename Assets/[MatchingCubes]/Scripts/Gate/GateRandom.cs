using UnityEngine;

public class GateRandom : GateBase
{
    [SerializeField] private StackTypesData stackTypesDatabase;

    public override void OnInteracted(IStacker stacker)
    {
        foreach (var stack in stacker.Stacks)
            stack.UpdateStackType(stackTypesDatabase.GetRandomStackType());

        base.OnInteracted(stacker);
    }
}
