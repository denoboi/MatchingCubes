using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelStartPanel : EasyPanel
{
    private void OnEnable()
    {
        LevelSystem.Instance.OnLevelStarted.AddListener(HidePanelAnimated);
        LevelSystem.Instance.OnLevelLoaded.AddListener(ShowPanelAnimated);
    }

    private void OnDisable()
    {
        LevelSystem.Instance.OnLevelStarted.RemoveListener(HidePanelAnimated);
        LevelSystem.Instance.OnLevelLoaded.RemoveListener(ShowPanelAnimated);
    }
}
