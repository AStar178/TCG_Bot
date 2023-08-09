using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class SpiritBox : MonoBehaviour
{
    [SerializeField]
    private GameObject Orb;
    [SerializeField]
    private GameObject Sword;

    public ParticleSystem Bulletref;
    public ParticleSystem Bulletrefd;
    [SerializeField]
    private ParticleSystem Trail;
    [SerializeField]
    private VisualEffect BlackHole;


    public void ActiveSword()
    {
        Sword.SetActive(true);
        Trail.Play();
        Orb.SetActive(false);
    }

    public void ActiveOrb()
    {
        Orb.SetActive(true);
        Sword.SetActive(false);
        Trail.Stop();
    }

    public void ActiveBlackHole(bool d)
    {
        if (d)
            BlackHole.Play();
        else
            BlackHole.Stop();
    }
}
