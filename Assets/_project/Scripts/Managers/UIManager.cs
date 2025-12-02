using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameDirector gameDirector;
    public MainMenu mainMenu;
    public WinUI winUI;
    public FailUI failUI;
    public void ShowMainMenu()
    {
        mainMenu.Show();
    }
    public void HideMainMenu()
    {
        mainMenu.Hide();
    }
    public void ShowInGameUI()
    {

    }
    public void HideInGameUI()
    {

    }
    public void ShowWinUI()
    {
        winUI.Show();
    }
    public void HideWinUI()
    {
        winUI.Hide();
    }
    public void ShowFailUI()
    {
        failUI.Show();
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
