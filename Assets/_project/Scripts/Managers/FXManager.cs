using System;
using UnityEngine;

public class FXManager : MonoBehaviour
{
    public ParticleSystem impactPS;
    public ParticleSystem potionCollectedPS;

    public void PlayImpactPS(Vector3 pos, Vector3 dir, Color color)
    {
        var newPS = Instantiate(impactPS);
        newPS.transform.position = pos + dir*.3f;
        newPS.transform.LookAt(pos + dir);
        var main = newPS.main;
        main.startColor = color;
        newPS.Play();
    }

    public void PlayPotionCollectedPS(Vector3 pos)
    {
        var newPS = Instantiate(potionCollectedPS);
        newPS.transform.position = pos + Vector3.up;
        newPS.Play();
    }
}
