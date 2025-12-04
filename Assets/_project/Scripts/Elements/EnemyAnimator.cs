using System;
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
            _animator.ResetTrigger("GetHit");
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

    public void PlayDieAnimation()
    {
        if (animationState != AnimationState.Die)
        {
            _animator.ResetTrigger("GetHit");
            _animator.SetTrigger("Die");
            animationState = AnimationState.Die;
        }
    }

    public void SetAnimationSpeed(float v)
    {
        _animator.speed = v;
    }

    public void PlayGetHitAnimation()
    {
        if (animationState != AnimationState.GetHit)
        {
            animationState = AnimationState.GetHit;
            _animator.SetTrigger("GetHit");
        }        
    }
}

public enum AnimationState
{
    Idle,
    Walk,
    Attack,
    Die,
    GetHit,
}