using System.Collections;
using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;
    public FXManager fXManager;
    public AudioManager audioManager;
    public TimerManager timerManager;
    public Player player;

    public UIManager uIManager;

    public GameState gameState;

    public float timeScale;


    private void Start()
    {        
        uIManager.ShowMainMenu();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            RestartLevel();
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
    public void RestartLevel()
    {
        gameState = GameState.GamePlay;
        levelManager.RestartLevelManager();
        player.RestartPlayer();
        audioManager.PlayAmbientSound();
        timerManager.RestartTimerManager(levelManager.GetCurrentLevelTime());
        uIManager.ShowInGameUI();
    }

    public void LevelCompleted()
    {
        gameState = GameState.Win;
        uIManager.ShowWinUI();
        audioManager.StopAmbientSound();
    }

    public void LevelFailed(float delay)
    {
        gameState = GameState.Lose;
        uIManager.ShowFailUI(delay);
        audioManager.StopAmbientSound();
    }


    void LoadPreviousLevel()
    {
        levelManager.levelNo--;
        RestartLevel();
    }
    public void LoadNextLevel()
    {
        levelManager.levelNo++;
        RestartLevel();
    }
}

public enum GameState
{
    MainMenu,
    GamePlay,
    Win,
    Lose,
}