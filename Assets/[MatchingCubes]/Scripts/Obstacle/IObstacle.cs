using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacle : IComponent
{
    public bool IsInteracted { get; set; }
    public void OnInteracted(IObstacleTarget target);

    public void Dispose();
}
