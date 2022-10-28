using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Scriptable Objects/Database/New Level Database", fileName = "LevelDatabase")]
public class LevelDatabase : ScriptableObject
{
    public Level[] levels;

    public int GetLevelIndexById(string id)
    {
        for (int i = 0; i < levels.Length; i++)
        {
            if (levels[i].levelId == id)
                return i;
        }

        return -1;
    }

    public string GetLevelIdByIndex(int index)
    {
        return levels[index].levelId;
    }

    public Level GetLevelByIndex(int index)
    {
        return levels[index];
    }
}
