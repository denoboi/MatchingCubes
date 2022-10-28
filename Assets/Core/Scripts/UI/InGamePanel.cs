using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGamePanel : EasyPanel
{
    private void OnEnable()
    {
        LevelSystem.Instance.OnLevelStarted.AddListener(ShowPanelAnimated);
        LevelSystem.Instance.OnLevelLoaded.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        LevelSystem.Instance.OnLevelStarted.RemoveListener(ShowPanelAnimated);
        LevelSystem.Instance.OnLevelLoaded.RemoveListener(HidePanel);
    }
}
