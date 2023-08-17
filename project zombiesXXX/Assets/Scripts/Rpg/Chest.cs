using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using static RpgHelper;

public class Chest : Interactable
{
    [SerializeField] Transform iteamHolder;
    [SerializeField] public GameObject Iteam;
    [HideInInspector]
    public StateScriptAbleObject stateScriptAbleObject;
    [SerializeField] Animation animationx;
    [SerializeField] IteamType iteamType;
    public ParticleSystem[] particleSystems;
    [SerializeField] Renderer[] particles;
    [SerializeField] Outliner outliner;
    GameObject w;
    private async void Start() {
        await Task.Delay(1000);
        if (Iteam != null)
            return;
        GetReady();

    }
    
    public void GetReady() {
        caninteracted = true;
        if (Iteam == null)
        {
            Iteam = Instantiate(RPGStatic.Instance.EmpetyIteam , iteamHolder.transform.position , Quaternion.identity);
            Iteam.GetComponent<IteamforChest>().stateScriptAbleObject = stateScriptAbleObject;
            Iteam.GetComponent<IteamforChest>().iteamTypo = stateScriptAbleObject.iteamTypo;
            w = Iteam;
        }
        else
            w = Instantiate(Iteam , iteamHolder.transform.position , Quaternion.identity);

        if (w.TryGetComponent<Iteam>(out var s))
            s.enabled = false;
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
    outliner.enabled = false;
    }
    public override void OnInteracted()
    {
        w.GetComponent<IteamforChest>().fixit();
        outliner.enabled = true;
        w.gameObject.layer = 10;
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
        Destroy(this , 6);
    }
    public void Des()
    {
        Destroy(w.transform.parent.gameObject);
        Destroy(this , 6);
    }
}
