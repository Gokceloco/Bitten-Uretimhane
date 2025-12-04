using UnityEngine;

public class TimerManager : MonoBehaviour
{
    public GameDirector gameDirector;
    private float _remainingTime;
    private float _totalTime;

    public TimerUI timerUI;

    public void RestartTimerManager(float levelTime)
    {
        _remainingTime = levelTime;
        _totalTime = levelTime;
    }

    private void Update()
    {
        if (gameDirector.gameState == GameState.GamePlay)
        {
            _remainingTime -= Time.deltaTime;
            timerUI.SetFillBar(_remainingTime / _totalTime, _remainingTime);
        }
        if (_remainingTime < 0 && gameDirector.gameState == GameState.GamePlay)
        {
            gameDirector.player.PlayAlternativeDieAnimation();
            gameDirector.LevelFailed(2);
        }
        print(_remainingTime);
    }
}
