using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StackableRandomCube : StackableBase
{
    [SerializeField] private StackTypesData stackTypeDatabase;

    public override void Start()
    {
        UpdateStackType(stackTypeDatabase.GetRandomStackType());
    }
}
