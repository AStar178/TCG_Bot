using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiritBox : MonoBehaviour
{
    [SerializeField]
    private GameObject Orb;
    [SerializeField]
    private GameObject Sword;

    public void ActiveSword()
    {
        Sword.SetActive(true);
        Orb.SetActive(false);
    }

    public void ActiveOrb()
    {
        Orb.SetActive(true);
        Sword.SetActive(false);
    }
}
