using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;

    public Player player;

    private void Start()
    {
        StartLevel();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            StartLevel();
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            LoadNextLevel();
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            LoadPreviousLevel();
        }
    }
    void StartLevel()
    {
        levelManager.RestartLevelManager();
        player.RestartPlayer();
    }
    void LoadPreviousLevel()
    {
        levelManager.levelNo--;
        StartLevel();
    }
    void LoadNextLevel()
    {
        levelManager.levelNo++;
        StartLevel();
    }
}
