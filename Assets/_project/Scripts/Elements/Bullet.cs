using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameDirector _gameDirector;
    public float speed;
    private Vector3 _startPos;

    public void StartBullet(GameDirector gameDirector)
    {
        _gameDirector = gameDirector;
        _startPos = transform.position;
    }

    private void Update()
    {
        transform.position 
            += transform.forward * speed * Time.deltaTime;

        if((transform.position - _startPos).magnitude > 50)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            _gameDirector.fXManager.PlayImpactPS(transform.position, -transform.forward, Color.yellow);
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            _gameDirector.fXManager.PlayZombieImpactPS(transform.position, transform.forward);
            other.GetComponent<Enemy>().GetHit();
            _gameDirector.audioManager.PlayImpactAS();
            Destroy(gameObject);    
        }
    }
}
