using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Systems : MonoBehaviour
{
#if UNITY_EDITOR
    private void OnApplicationQuit()
    {
        foreach (var script in FindObjectsOfType<MonoBehaviour>())
        {
            script.enabled = false;
        }
    }
#endif
}
