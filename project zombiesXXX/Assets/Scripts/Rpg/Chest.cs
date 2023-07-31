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
    GameObject w;
    private void Start() {
        caninteracted = true;
        w = Instantiate(Iteam , iteamHolder.transform.position , Quaternion.identity);
        w.GetComponent<Iteam>().enabled = false;
        iteamType = w.GetComponent<IteamforChest>().iteamTypo;
        w.GetComponent<IteamforChest>().chest = this;
        w.transform.SetParent(iteamHolder.transform);
        w.transform.localPosition = Vector3.zero;
        
        for (int i = 0; i < particles.Length; i++)
        {
            List<Material> f = new List<Material>();
            f.Add(RPGStatic.Instance.GetRightMatrialColorForIteamRarety(iteamType));
            f.Add(RPGStatic.Instance.GetRightMatrialColorForIteamRarety(iteamType));
            particles[i].SetMaterials(f);
            particles[i].material = RPGStatic.Instance.GetRightMatrialColorForIteamRarety(iteamType);
        }
        outliner.OutlineColor = RPGStatic.Instance.GetRightColorForIteamRarety(iteamType);
    outliner.Awake();
    outliner.OnEnable();
    }
    public override void OnInteracted()
    {
        w.GetComponent<IteamforChest>().fixit();
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
    public void Des()
    {
        Destroy(w.transform.parent.gameObject);
        Destroy(this);
    }
}
