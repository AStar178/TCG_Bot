using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
