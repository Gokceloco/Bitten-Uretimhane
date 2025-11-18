using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;

    public Player player;

    public GameState gameState;

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
        gameState = GameState.GamePlay;
        levelManager.RestartLevelManager();
        player.RestartPlayer();
    }

    public void LevelCompleted()
    {
        gameState = GameState.Win;
        Invoke(nameof(LoadNextLevel), 1);
    }

    public void LevelFailed()
    {
        gameState = GameState.Lose;
        Invoke(nameof(StartLevel), 1);
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

public enum GameState
{
    MainMenu,
    GamePlay,
    Win,
    Lose,
}