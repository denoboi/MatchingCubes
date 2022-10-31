using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GateBase : MonoBehaviour, IGate
{
    public virtual void OnInteracted(IStacker stacker)
    {
        Dispose();
    }

    public virtual void Dispose()
    {
        Destroy(gameObject);
    }
}
