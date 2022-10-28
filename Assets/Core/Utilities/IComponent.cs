using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IComponent
{
    public Transform transform { get; }
    public GameObject gameObject { get; }
}
