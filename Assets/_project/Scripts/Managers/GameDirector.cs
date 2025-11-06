using UnityEngine;

public class GameDirector : MonoBehaviour
{
    public LevelManager levelManager;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            levelManager.RestartLevelManager();
        }
    }
}
