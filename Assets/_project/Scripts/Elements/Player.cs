using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameDirector gameDirector;

    public int startHealth;
    private int _currentHealth;

    public HealthBar healthBar;

    public SpriteRenderer shadowSR;

    public LayerMask shadowLayerMask;

    private PlayerAnimator _playerAnimator;

    private void Awake()
    {
        _playerAnimator = GetComponent<PlayerAnimator>();
    }

    public void RestartPlayer()
    {
        gameObject.SetActive(true);
        transform.position = Vector3.zero;
        _currentHealth = startHealth;
        _playerAnimator.ChangeAnimationState("Idle");
        shadowSR.enabled = true;
        GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation;
    }

    public void GetHit(int damage)
    {
        _currentHealth -= damage;
        healthBar.SetFillAmount((float)_currentHealth / startHealth);
        if (_currentHealth <= 0)
        {
            gameDirector.LevelFailed(2);
            _playerAnimator.ChangeAnimationState("Die");
            shadowSR.enabled = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Potion"))
        {
            other.gameObject.SetActive(false);
            gameDirector.LevelCompleted();
            gameDirector.fXManager.PlayPotionCollectedPS(other.transform.position);
            _playerAnimator.ChangeAnimationState("Drink");
            gameDirector.audioManager.PlayPositiveAS();
        }
    }

    private void Update()
    {
        CheckIfFellDown();
        SetShadowPositionAndScale();
    }

    private void CheckIfFellDown()
    {
        if (transform.position.y < -10f && gameDirector.gameState == GameState.GamePlay)
        {
            gameDirector.LevelFailed(0);
        }
    }
    

    private void SetShadowPositionAndScale()
    {
        if (Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, out var hit, 10, shadowLayerMask)
            && gameDirector.gameState == GameState.GamePlay)
        {
            shadowSR.gameObject.SetActive(true);
            shadowSR.transform.position = hit.point + Vector3.up * .01f;
        }
        else
        {
            shadowSR.gameObject.SetActive(false);
        }
        
        var distance = (shadowSR.transform.position - transform.position).magnitude;

        shadowSR.transform.localScale = Vector3.one + distance * Vector3.one;

        shadowSR.color = new Color(0,0,0,1 - distance * .3f);
    }

    public void PlayAlternativeDieAnimation()
    {
        _playerAnimator.ChangeAnimationState("Die2");
    }
}
