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
        CheckIfFellDown();
        SetShadowPositionAndScale();
    }

    private void CheckIfFellDown()
    {
        if (transform.position.y < -10f && gameDirector.gameState == GameState.GamePlay)
        {
            gameDirector.LevelFailed();
        }
    }
    

    private void SetShadowPositionAndScale()
    {
        if (Physics.Raycast(transform.position + Vector3.up * .1f, Vector3.down, out var hit, 10, shadowLayerMask))
        {
            shadowSR.transform.position = hit.point + Vector3.up * .01f;
        }
        var distance = (shadowSR.transform.position - transform.position).magnitude;

        shadowSR.transform.localScale = Vector3.one + distance * Vector3.one;

        shadowSR.color = new Color(0,0,0,1 - distance * .3f);
    }
}
