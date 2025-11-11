using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public Bullet bulletPrefab;

    public Transform shootPosition;
    public float attackRate;

    private float _lastShootTime;

    private void Update()
    {      
        if (Input.GetMouseButton(0) 
            && Time.time - _lastShootTime > attackRate)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        var newBullet = Instantiate(bulletPrefab);
        newBullet.transform.position = shootPosition.position;
        newBullet.transform.LookAt(
            shootPosition.position + shootPosition.forward);
        _lastShootTime = Time.time;
    }
}
