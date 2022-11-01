using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCube : ObstacleBase
{
    [SerializeField] private Rigidbody[] cubeRigidbodies;
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

        foreach (var rb in cubeRigidbodies)
        {
            rb.isKinematic = false;
            rb.AddForce(new Vector3(Random.Range(-1, 2), 1, Random.Range(-1, 2)) * 250);
        }
    }
}
