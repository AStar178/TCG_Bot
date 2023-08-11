using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZAWARDO : IteamSkill
{
    public static ZAWARDO Current;
    [SerializeField] float ZawardoTime;
    [SerializeField] float ColdDown;
    [SerializeField] AudioSource audioSource;
    [SerializeField] GameObject Effectl;
    public bool ZawardoTimeded;    
    float t;
    float x;
    float stop;
    public override void OnStart(PlayerState playerState)
    {

        Current = this;

    }
    public override void OnUseSkill(PlayerState playerState)
    {

        if (x > 0)  
            return;
        
        ZawardoTimeded = true;
        t = ZawardoTime;
        x = ColdDown;
        Icons.SetCooldown(x, ColdDown);
        var s = Instantiate(Effectl , PlayerGetpos , Quaternion.identity);
        Destroy(s , 2);
        audioSource.Play();

    }

    private void Update() {
        
        x -= Time.deltaTime;
        t -= Time.deltaTime;
        stop -= Time.deltaTime;
        Icons.SetCooldown(x, ColdDown);   

        if (t > 0)
        {
            
            PostProcsingControler.Current.colorAdjustments.postExposure.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.postExposure.value , -1 , 10 * Time.deltaTime );
            PostProcsingControler.Current.colorAdjustments.hueShift.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.hueShift.value , -180 , 10 * Time.deltaTime );
            PostProcsingControler.Current.colorAdjustments.saturation.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.saturation.value , -100 , 10 * Time.deltaTime );
            stop = 5;
            return;
        }
        if (stop < 0)
            return;
        PostProcsingControler.Current.colorAdjustments.postExposure.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.postExposure.value , 0 , 10 * Time.deltaTime );
        PostProcsingControler.Current.colorAdjustments.hueShift.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.hueShift.value , 0 , 10 * Time.deltaTime );
        PostProcsingControler.Current.colorAdjustments.saturation.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.saturation.value , 0 , 10 * Time.deltaTime );
        ZawardoTimeded = false;
    }
}
