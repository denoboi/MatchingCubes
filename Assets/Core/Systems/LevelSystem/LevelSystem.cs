using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Sirenix.OdinInspector;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSystem : Singleton<LevelSystem>
{
    [SerializeField] private LevelDatabase levelDatabase;

    public Level CurrentLevel { get; private set; }

    public bool IsLevelStarted { get; set; }

    private int currentLevelIndex;
    private bool isLevelLoading;

    public Event OnLevelLoadingStarted = new Event();
    public Event OnLevelLoaded = new Event();
    public Event OnLevelStarted = new Event();
    public Event OnLevelFinished = new Event();

    public void StartLevel()
    {
        if (IsLevelStarted) return;

        IsLevelStarted = true;
        OnLevelStarted.Invoke();
    }

    public void FinishLevel()
    {
        if (!IsLevelStarted) return;

        IsLevelStarted = false;
        OnLevelFinished.Invoke();
    }

    private void LoadLevel(int levelIndex)
    {
        if (isLevelLoading) return;

        StartCoroutine(LoadLevelCo(levelIndex));
    }

    private IEnumerator LoadLevelCo(int levelIndex)
    {
        IsLevelStarted = false;
        isLevelLoading = true;
        OnLevelLoadingStarted.Invoke();
        yield return new WaitForSeconds(1f);
        Level targetLevel = levelDatabase.GetLevelByIndex(levelIndex);
        yield return SceneManager.LoadSceneAsync(targetLevel.levelId);
        yield return new WaitForSeconds(0.5f);
        currentLevelIndex = levelIndex;
        SaveLoadSystem.SetInt("LastLevelIndex", currentLevelIndex);
        CurrentLevel = targetLevel;
        OnLevelLoaded.Invoke();
        isLevelLoading = false;
    }

    public void LoadLastLevel()
    {
        LoadLevel(GetLastLevelIndex());
    }

    [Button]
    public void LoadNextLevel()
    {
        int nextLevelIndex = currentLevelIndex + 1;

        if (nextLevelIndex > GetLevelCount())
            LoadLevel(0);
        else
            LoadLevel(nextLevelIndex);
    }

    [Button]
    public void LoadPreviousLevel()
    {
        int previousLevelIndex = currentLevelIndex - 1;

        if (previousLevelIndex < 0)
            LoadLevel(0);
        else
            LoadLevel(previousLevelIndex);
    }

    [Button]
    public void RestartLevel()
    {
        LoadLevel(currentLevelIndex);
    }

    public void LoadCurrentEditorLevel()
    {
        currentLevelIndex = levelDatabase.GetLevelIndexById(SceneManager.GetActiveScene().name);
        CurrentLevel = levelDatabase.GetLevelByIndex(currentLevelIndex);
        OnLevelLoaded.Invoke();
    }

    public int GetLevelCount()
    {
        return levelDatabase.levels.Length - 1;
    }

    public int GetLastLevelIndex()
    {
        return SaveLoadSystem.GetInt("LastLevelIndex", 0);
    }
}

public enum LevelType
{
    Normal,
    Tutorial
}

[System.Serializable]
public struct Level
{
    [ValueDropdown("GetScenesInBuildSettings")]
    public string levelId;
    public LevelType levelType;

#if UNITY_EDITOR
    private IEnumerable<string> GetScenesInBuildSettings()
    {
        string[] scenes = EditorBuildSettings.scenes
            .Where(scene => scene.enabled)
            .Select(scene => Path.GetFileNameWithoutExtension(scene.path))
            .ToArray();

        return scenes;
    }
#endif
}
