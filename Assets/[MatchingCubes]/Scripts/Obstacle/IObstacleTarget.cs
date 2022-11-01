using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObstacleTarget : IComponent
{
    bool IsImmune { get; set; }
}
