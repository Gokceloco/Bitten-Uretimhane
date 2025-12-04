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

    private Coroutine _attackCoroutine;
    private Coroutine _getHitCoroutine;

    public List<Light> lights;

    private bool _didNoticePlayer;
    private bool _isGettingHit;

    private Collider _enemyCollider;

    public void StartEnemy(GameDirector gameDirector)
    {
        _currentHealth = startHealth;
        _gameDirector = gameDirector;
        _navmeshAgent = GetComponent<NavMeshAgent>();
        _enemyAnimator = GetComponent<EnemyAnimator>();
        _enemyCollider = GetComponent<CapsuleCollider>();
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
            if (!_didNoticePlayer)
            {
                _didNoticePlayer = true;
                _gameDirector.audioManager.PlayZombieScreamAS();
            }
        }

        //Action
        if (actionState == ActionState.WalkingTowardsPlayer && !_isGettingHit)
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
                _attackCoroutine = StartCoroutine(AttackPlayerCoroutine());
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
        _getHitCoroutine = StartCoroutine(GetHitCoroutine());
        if (actionState == ActionState.Standing)
        {
            actionState = ActionState.WalkingTowardsPlayer;
        }
        if (_currentHealth <= 0)
        {
            Die();
        }
    }

    IEnumerator GetHitCoroutine()
    {
        _navmeshAgent.isStopped = true;
        _isGettingHit = true;
        if (actionState == ActionState.WalkingTowardsPlayer)
        {
            _enemyAnimator.PlayGetHitAnimation();
        }

        yield return new WaitForSeconds(.15f);

        _navmeshAgent.isStopped = false;
        _isGettingHit = false;
        _enemyAnimator.PlayWalkAnimation();
    }
    private void Die()
    {
        if (_attackCoroutine != null)
        {
            StopCoroutine(_attackCoroutine);
        }
        if (_getHitCoroutine != null)
        {
            StopCoroutine(_getHitCoroutine);
        }
        actionState = ActionState.Dying;
        _enemyAnimator.PlayDieAnimation();
        foreach (var l in lights)
        {
            l.enabled = false;
        }
        _enemyCollider.enabled = false;
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