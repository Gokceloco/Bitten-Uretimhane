using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;

    public int startHealth;
    private int _currentHealth;

    public HealthBar healthBar;

    public void RestartPlayer()
    {
        gameObject.SetActive(true);
        transform.position = Vector3.zero;
        _currentHealth = startHealth;
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        healthBar.SetFillAmount((float)_currentHealth / startHealth);
        if (_currentHealth <= 0)
        {
            gameObject.SetActive(false);
            gameDirector.LevelFailed();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            other.gameObject.SetActive(false);
            gameDirector.LevelCompleted();
        }
    }

    private void Update()
    {
        if (transform.position.y < -10f && gameDirector.gameState == GameState.GamePlay)
        {
            gameDirector.LevelFailed();
        }
    }
}
