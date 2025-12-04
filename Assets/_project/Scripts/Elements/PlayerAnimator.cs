using System;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    public void ChangeAnimationState(string key)
    {
        _animator.SetBool("Run", false);
        _animator.SetBool("Idle", false);
        _animator.SetBool("Die", false);
        _animator.SetBool("Die2", false);
        _animator.SetBool("Drink", false);
        _animator.SetBool(key, true);
    }

    public void SetRunDirection(float angle)
    {
        _animator.SetFloat("RunDirection", angle);
    }
}
