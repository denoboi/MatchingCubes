using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StackType_X", menuName = "Scriptable Objects/Stack Type")]
public class StackTypeSO : ScriptableObject
{
    public Color Color;
    public ParticleSystem DisposeParticle;
}
