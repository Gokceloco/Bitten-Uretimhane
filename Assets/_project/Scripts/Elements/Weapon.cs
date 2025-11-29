using DG.Tweening;
using System;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public GameDirector gameDirector;
    public Bullet bulletPrefab;

    public Transform shootPosition;
    public float attackRate;

    private float _lastShootTime;

    public Light muzzleLight;
    public ParticleSystem muzzlePS;

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
        newBullet.StartBullet(gameDirector);
        
        muzzleLight.DOKill();
        muzzleLight.intensity = 0;
        muzzleLight.DOIntensity(20,.05f).SetLoops(2, LoopType.Yoyo);

        muzzlePS.Play();
    }
}
