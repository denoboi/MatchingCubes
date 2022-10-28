using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashPanel : EasyPanel
{
    private void OnEnable()
    {
        LevelSystem.Instance.OnLevelLoaded.AddListener(OnLevelLoaded);
    }

    private void OnDisable()
    {
        LevelSystem.Instance.OnLevelLoaded.RemoveListener(OnLevelLoaded);
    }

    private void Awake()
    {
        ShowPanel();
    }

    private void OnLevelLoaded()
    {
        HidePanel();
        Destroy(gameObject);
    }
}
