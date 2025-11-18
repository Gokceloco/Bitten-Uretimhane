using System;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameDirector gameDirector;

    public int levelNo;

    public List<Level> levelPrefabs;

    private Level _curLevel;

    public void RestartLevelManager()
    {
        DestroyPreviousLevel();
        CreateNewLevel();
    }

    private void DestroyPreviousLevel()
    {
        if (_curLevel != null)
        {
            Destroy(_curLevel.gameObject);
        }
    }

    private void CreateNewLevel()
    {
        levelNo = Math.Clamp(levelNo, 1, levelPrefabs.Count);
        _curLevel = Instantiate(levelPrefabs[levelNo-1]);
        _curLevel.StartLevel(gameDirector);
    }
}
