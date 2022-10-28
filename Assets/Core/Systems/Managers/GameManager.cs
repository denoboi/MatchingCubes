using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    private bool isLevelCompleted;

    public Event<bool> OnLevelCompleted = new Event<bool>();

    private void OnEnable()
    {
        LevelSystem.Instance.OnLevelLoaded.AddListener(ResetLevelCompletionStatus);
    }

    private void OnDisable()
    {
        LevelSystem.Instance.OnLevelLoaded.RemoveListener(ResetLevelCompletionStatus);
    }

    [Button]
    public void CompleteLevel(bool isSuccess)
    {
        if (isLevelCompleted) return;

        isLevelCompleted = true;

        OnLevelCompleted.Invoke(isSuccess);
        LevelSystem.Instance.FinishLevel();
        if (isSuccess)
            SaveLoadSystem.SetInt("FakeLevel", SaveLoadSystem.GetInt("FakeLevel", 1) + 1);
    }

    private void ResetLevelCompletionStatus()
    {
        isLevelCompleted = false;
    }
}
