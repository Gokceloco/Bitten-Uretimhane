using System;
using UnityEngine;

public class Level : MonoBehaviour
{
    private GameDirector _gameDirector;

    public float levelTime;

    public void StartLevel(GameDirector gameDirector)
    {
        _gameDirector = gameDirector;
        foreach (var e in GetComponentsInChildren<Enemy>())
        {
            e.StartEnemy(_gameDirector);
        }
    }
}
