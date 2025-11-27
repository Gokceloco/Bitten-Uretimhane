using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    private GameDirector _gameDirector;

    public int startHealth;
    private int _currentHealth;
    public HealthBar healthBar;
    public ActionState actionState;
    private NavMeshAgent _navmeshAgent;
    public LayerMask seeThroughLayerMask;
    private bool _isAttackInProgress;
    public float attackRate;

    private EnemyAnimator _enemyAnimator;

    private Coroutine _shootCoroutine;

    public List<Light> lights;

    public void StartEnemy(GameDirector gameDirector)
    {
        _currentHealth = startHealth;
        _gameDirector = gameDirector;
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
    }
    private void Update()
    {
        if (actionState == ActionState.Dying)
        {
            _navmeshAgent.isStopped = true;
            return;
        }

        var playerPos = _gameDirector.player.transform.position;
        var distance = (playerPos - transform.position).magnitude;
        var direction = (playerPos - transform.position).normalized;


        //Decider Logic
        if (distance < 2)
        {
            actionState = ActionState.Attacking;
        }
        else if (!_isAttackInProgress && distance < 10 && CanSeePlayer(direction))
        {
            actionState = ActionState.WalkingTowardsPlayer;            
        }

        //Action
        if (actionState == ActionState.WalkingTowardsPlayer)
        {
            _navmeshAgent.SetDestination(playerPos);
            _navmeshAgent.isStopped = false;
            _enemyAnimator.PlayWalkAnimation();
        }
        else if (actionState == ActionState.Attacking)
        {
            _navmeshAgent.isStopped = true;
            if (!_isAttackInProgress)
            {
                _shootCoroutine = StartCoroutine(AttackPlayerCoroutine());
            }            
        }
    }
    IEnumerator AttackPlayerCoroutine()
    {
        _isAttackInProgress = true;
        _enemyAnimator.PlayAttackAnimation();
        yield return new WaitForSeconds(attackRate);

        var damage = 1;

        if (Random.value < .2f)
        {
            damage = 2;
        }

        var playerPos = _gameDirector.player.transform.position;
        var distance = (playerPos - transform.position).magnitude;
        if (distance < 2)
        {
            _gameDirector.player.GetHit(damage);
        }

        _isAttackInProgress = false;
    }
    private bool CanSeePlayer(Vector3 direction)
    {
        if (Physics.Raycast(transform.position + Vector3.up, direction, 10, seeThroughLayerMask))
        {
            return false;
        }

        return true;
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
        if (_shootCoroutine != null)
        {
            StopCoroutine(_shootCoroutine);
        }
        actionState = ActionState.Dying;
        _enemyAnimator.PlayDieAnimation();
        foreach (var l in lights)
        {
            l.enabled = false;
        }
        Destroy(gameObject, 3);
    }
}
public enum ActionState
{
    Standing,
    WalkingTowardsPlayer,
    Attacking,
    Dying,
}
public enum EnemyType
{
    Knight,
    Boss1,
}