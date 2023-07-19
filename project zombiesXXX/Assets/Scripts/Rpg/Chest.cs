using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    [SerializeField] Transform iteamHolder;
    [SerializeField] StateScriptAbleObject stateScriptAbleObject;
    [SerializeField] Animation animationx;
    public ParticleSystem[] particleSystems;
    public override void OnInteracted()
    {
        OpeanChest();
    }
    private void OpeanChest()
    {
        //every iteam have uniqe color particale
        animationx.Play();
        caninteracted = false;

    }
    public void SpawnParticale()
    {
        for (int i = 0; i < particleSystems.Length; i++)
        {
            particleSystems[i].Play();
        }
        Destroy(this);
    }
    
}
