using UnityEngine;

public class PlayerEventHandler : MonoBehaviour
{
    public GameDirector gameDirector;
    public void Step()
    {
        gameDirector.audioManager.PlayStepAS();
    }
}
