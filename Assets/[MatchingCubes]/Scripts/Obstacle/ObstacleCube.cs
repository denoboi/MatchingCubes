using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleCube : ObstacleBase
{
    private Rigidbody rb;
    public Rigidbody Rb { get { return rb == null ? rb = GetComponent<Rigidbody>() : rb; } }

    public override void Dispose()
    {
        Rb.isKinematic = false;
        Rb.AddForce(new Vector3(Random.Range(-1, 2), 1, Random.Range(-1, 2)) * 100);
    }
}
