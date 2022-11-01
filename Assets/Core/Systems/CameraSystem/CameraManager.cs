using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using Cinemachine;

public class CameraManager : Singleton<CameraManager>
{
    private List<VCam> cameras = new List<VCam>();
    [SerializeField] private bool debugMode;
    private Camera mainCam;
    public Camera MainCam { get { return mainCam == null ? mainCam = Camera.main : mainCam; } }

    private VCam currentCam;

    public void AssignCamera(VCam cam)
    {
        if (!cameras.Contains(cam))
        {
            cameras.Add(cam);
            if (cam.IsStartingCamera)
                SwitchCam(cam.CamID, 0);
        }
    }

    public void UnassignCamera(VCam cam)
    {
        if (cameras.Contains(cam))
            cameras.Remove(cam);
    }

    public void SwitchCam(string camID, float blendDuration = 4f, float blendDelay = 0f)
    {
        StartCoroutine(SwitchCamCo(camID, blendDuration, blendDelay));
    }

    private IEnumerator SwitchCamCo(string camID, float blendDuration = 4f, float blendDelay = 0f)
    {
        yield return new WaitForSeconds(blendDelay);

        if (currentCam != null && camID == currentCam.CamID)
            yield break;

        if (debugMode)
            Debug.Log("Camera is switching to : " + camID);

        CinemachineBrain brain = MainCam.GetComponent<CinemachineBrain>();
        brain.m_DefaultBlend.m_Time = blendDuration;
        bool isCamIDExists = false;

        foreach (var cam in cameras)
        {
            if (cam.CamID == camID)
            {
                cam.Vc.Priority = 11;
                isCamIDExists = true;
                currentCam = cam;
            }
            else
                cam.Vc.Priority = 10;
        }

        if (!isCamIDExists)
            Debug.LogWarning("There is no camera with ID (" + isCamIDExists + ")");
    }
}