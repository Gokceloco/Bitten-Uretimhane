using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private GameDirector _gameDirector;

    public int startHealth;
    private int _currentHealth;

    public HealthBar healthBar;

    public float speed;

    public ActionState actionState;

    public void StartEnemy(GameDirector gameDirector)
    {
        _currentHealth = startHealth;
        _gameDirector = gameDirector;
    }

    private void Update()
    {
        var playerPos = _gameDirector.player.transform.position;
        var distance = (playerPos - transform.position).magnitude;
        var direction = (playerPos - transform.position).normalized;

        //Decider Logic
        if (distance < 2)
        {
            actionState = ActionState.Attacking;
        }
        else if (distance < 10)
        {
            actionState = ActionState.WalkingTowardsPlayer;
        }




        //Action
        if (actionState == ActionState.WalkingTowardsPlayer)
        {
            transform.position += direction * speed * Time.deltaTime;
        }       



    }

    public void GetHit()
    {
        _currentHealth--;
        healthBar.SetFillAmount((float)_currentHealth / startHealth);
        if (actionState == ActionState.Standing)
        {
            actionState = ActionState.WalkingTowardsPlayer;
        }
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}


public enum ActionState
{
    Standing,
    WalkingTowardsPlayer,
    Attacking,
    Dying,
}