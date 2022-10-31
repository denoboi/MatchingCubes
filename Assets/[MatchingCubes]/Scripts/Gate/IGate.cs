using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGate : IComponent
{
    void OnInteracted(IStacker stacker);
    void Dispose();
}
