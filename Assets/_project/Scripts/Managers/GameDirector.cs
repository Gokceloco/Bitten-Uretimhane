using System.Collections;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public FXManager fXManager;
    public AudioManager audioManager;
    public Player player;

    public GameState gameState;

    public float timeScale;


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

        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(BulletTimeCoroutine());
        }
    }

    IEnumerator BulletTimeCoroutine()
    {
        Time.timeScale = .3f;
        yield return new WaitForSeconds(2 * .3f);
        Time.timeScale = 1;
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
        Invoke(nameof(LoadNextLevel), 2);
    }

    public void LevelFailed()
    {
        gameState = GameState.Lose;
        Invoke(nameof(StartLevel), 2);
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