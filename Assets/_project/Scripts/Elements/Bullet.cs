using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed;
    private Vector3 _startPos;
    private void Start()
    {
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
            Destroy(gameObject);
        }
        if (other.CompareTag("Enemy"))
        {
            other.GetComponent<Enemy>().GetHit();
            Destroy(gameObject);            
        }
    }
}
