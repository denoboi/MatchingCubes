using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SuccessPanel : EasyPanel
{
    private void OnEnable()
    {
        GameManager.Instance.OnLevelCompleted.AddListener(TogglePanel);
        LevelSystem.Instance.OnLevelLoaded.AddListener(HidePanel);
    }

    private void OnDisable()
    {
        GameManager.Instance.OnLevelCompleted.RemoveListener(TogglePanel);
        LevelSystem.Instance.OnLevelLoaded.RemoveListener(HidePanel);
    }

    private void Awake()
    {
        HidePanel();
    }

    private void TogglePanel(bool isSuccess)
    {
        if (isSuccess)
            ShowPanelAnimated();
    }

    public void NextLevelButton()
    {
        HidePanelAnimated();
        LevelSystem.Instance.LoadNextLevel();
    }
}
