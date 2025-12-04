using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public MainMenu mainMenu;
    public WinUI winUI;
    public FailUI failUI;
    public TimerUI timerUI;
    public void ShowMainMenu()
    {
        gameDirector.gameState = GameState.MainMenu;
        mainMenu.Show();
        winUI.Hide();
        failUI.Hide();
        HideInGameUI();
    }
    public void HideMainMenu()
    {
        mainMenu.Hide();
    }
    public void ShowInGameUI()
    {
        timerUI.Show();
    }
    public void HideInGameUI()
    {
        timerUI.Hide();
    }
    public void ShowWinUI()
    {
        winUI.Show();
        HideInGameUI();
    }
    public void HideWinUI()
    {
        winUI.Hide();
    }
    public void ShowFailUI(float delay)
    {
        failUI.Show(delay);
        HideInGameUI();
    }
    public void HideFailUI()
    {
        failUI.Hide();
    }
    public void StartGameButtonPressed()
    {
        HideMainMenu();
        gameDirector.RestartLevel();
    }
    public void LoadNextLevelButtonPressed()
    {
        HideWinUI();
        gameDirector.LoadNextLevel();
    }
    public void RetryButtonPressed()
    {
        HideFailUI();
        gameDirector.RestartLevel();
    }
}
