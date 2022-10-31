using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObstacleBase : MonoBehaviour, IObstacle
{
    public bool IsInteracted { get; set; }

    public virtual void OnInteracted(IObstacleTarget target)
    {
        if (IsInteracted) return;

        if (target.transform.TryGetComponent(out IStackable stack))
        {
            stack.Stacker.RemoveStack(stack);
            IsInteracted = true;
        }
    }
    public abstract void Dispose();
}
