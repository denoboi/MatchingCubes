using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;

public class VCam : MonoBehaviour
{
    private CinemachineVirtualCamera vc;
    public CinemachineVirtualCamera Vc { get { return vc == null ? vc = GetComponent<CinemachineVirtualCamera>() : vc; } }

    public string CamID;
    public bool IsStartingCamera;

    private void OnEnable()
    {
        CameraManager.Instance.AssignCamera(this);
    }

    private void OnDisable()
    {
        CameraManager.Instance.UnassignCamera(this);
    }
}