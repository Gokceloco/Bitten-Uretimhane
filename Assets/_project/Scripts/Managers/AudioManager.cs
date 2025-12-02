using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gunShotAS;
    public AudioSource impactAS;
    public AudioSource stepAS;
    public AudioSource positiveAS;
    public AudioSource zombieScreamAS;
    public AudioSource ambientAS;

    public void PlayGunShotAS()
    {
        gunShotAS.Play();
    }
    public void PlayImpactAS()
    {
        impactAS.Play();
    }
    public void PlayStepAS()
    {
        stepAS.Play();
    }
    public void PlayPositiveAS()
    {
        positiveAS.Play();
    }
    public void PlayZombieScreamAS()
    {
        zombieScreamAS.Play();
    }
    public void PlayAmbientSound()
    {
        ambientAS.Play();
    }
    public void StopAmbientSound()
    {
        ambientAS.Stop();
    }
}