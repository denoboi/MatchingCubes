using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCube : ObstacleBase
{
    private Rigidbody rb;
    public Rigidbody Rb { get { return rb == null ? rb = GetComponent<Rigidbody>() : rb; } }

    private bool isDisposed;

    public override void Dispose()
    {
        if (isDisposed) return;

        isDisposed = true;

        var colliders = Physics.OverlapSphere(transform.position, 2);

        foreach (var col in colliders)
        {
            if (col.TryGetComponent(out IObstacle obstacle))
                obstacle.Dispose();
        }

        Rb.isKinematic = false;
        Rb.AddForce(new Vector3(Random.Range(-1, 2), 1, Random.Range(-1, 2)) * 100);
    }
}
