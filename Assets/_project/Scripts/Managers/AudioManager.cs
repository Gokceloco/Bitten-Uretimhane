using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource gunShotAS;
    public AudioSource impactAS;
    public AudioSource stepAS;

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
}
