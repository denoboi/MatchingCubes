using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BoostBase : MonoBehaviour, IBoost
{
    [SerializeField] protected float boostDuration;

    public abstract void Use(Transform user);
}
