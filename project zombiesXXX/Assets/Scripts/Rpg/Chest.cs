using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RpgHelper;

public class Chest : Interactable
{
    [SerializeField] Transform iteamHolder;
    [SerializeField] GameObject Iteam;
    [SerializeField] Animation animationx;
    [SerializeField] IteamType iteamType;
    public ParticleSystem[] particleSystems;
    [SerializeField] Renderer[] particles;
    [SerializeField] Outliner outliner;
    private void Start() {
        var w = Instantiate(Iteam , iteamHolder.transform.position , Quaternion.identity);
        Destroy( w.GetComponent<Iteam>() );
        iteamType = w.GetComponent<IteamforChest>().iteamTypo;
        w.transform.SetParent(iteamHolder.transform);
        w.transform.localPosition = Vector3.zero;
        
        for (int i = 0; i < particles.Length; i++)
        {
            List<Material> f = new List<Material>();
            f.Add(Minus.Instance.GetRightMatrialColorForIteamRarety(iteamType));
            f.Add(Minus.Instance.GetRightMatrialColorForIteamRarety(iteamType));
            particles[i].SetMaterials(f);
            particles[i].material = Minus.Instance.GetRightMatrialColorForIteamRarety(iteamType);
        }
        outliner.OutlineColor = Minus.Instance.GetRightColorForIteamRarety(iteamType);
    outliner.Awake();
    outliner.OnEnable();
    }
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
