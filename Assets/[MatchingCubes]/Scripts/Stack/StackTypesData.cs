using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackTypeDatabase", menuName = "Scriptable Objects/Stack Database")]
public class StackTypesData : ScriptableObject
{
    public StackTypeSO[] StackTypes;

    public StackTypeSO GetRandomStackType()
    {
        return StackTypes[Random.Range(0, StackTypes.Length)];
    }
}
