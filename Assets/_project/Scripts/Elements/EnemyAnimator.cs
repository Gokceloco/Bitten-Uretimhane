using UnityEngine;

public class EnemyAnimator : MonoBehaviour
{
    public AnimationState animationState;

    private Animator _animator;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void PlayWalkAnimation()
    {
        if (animationState != AnimationState.Walk)
        {
            _animator.SetTrigger("Walk");
            animationState = AnimationState.Walk;
        }
    }

    public void PlayIdleAnimation()
    {
        if (animationState != AnimationState.Idle)
        {
            _animator.SetTrigger("Idle");
            animationState = AnimationState.Idle;
        }
    }
    public void PlayAttackAnimation()
    {
        _animator.SetTrigger("Attack");
        animationState = AnimationState.Attack;
    }
}

public enum AnimationState
{
    Idle,
    Walk,
    Attack,
}