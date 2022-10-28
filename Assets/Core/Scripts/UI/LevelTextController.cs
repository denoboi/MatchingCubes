using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LevelTextController : MonoBehaviour
{
    private TextMeshProUGUI text;
    public TextMeshProUGUI Text { get { return text == null ? text = GetComponent<TextMeshProUGUI>() : text; } }
    private void OnEnable()
    {
        LevelSystem.Instance.OnLevelLoaded.AddListener(UpdateLevelText);
    }

    private void OnDisable()
    {
        LevelSystem.Instance.OnLevelLoaded.RemoveListener(UpdateLevelText);
    }

    private void UpdateLevelText()
    {
        Text.SetText("Level " + SaveLoadSystem.GetInt("FakeLevel", 1));
    }
}
