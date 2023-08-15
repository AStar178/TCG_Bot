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
    [SerializeField] private Material material;
    [SerializeField] private Material material2;
    public bool ZawardoTimeded;
    string Speed1 = "_FoamSpeed"; // 0.25
    string Speed2 = "_WaterSpeed"; // 0.02
    string Speed3 = "_WaveSpeed"; // 0.1
    string Speed4 = "_wind"; // 0.4
    string Speed5 = "_wind_1"; // 1   
    float t;
    float x;
    float stop;
    float wp;
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
        wp = 0;
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
            wp += Time.deltaTime;
            stop = 5;
            PostProcsingControler.Current.colorAdjustments.postExposure.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.postExposure.value , -1 , 10 * Time.deltaTime );
            PostProcsingControler.Current.colorAdjustments.hueShift.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.hueShift.value , -180 , 10 * Time.deltaTime );
            PostProcsingControler.Current.colorAdjustments.saturation.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.saturation.value , -100 , 10 * Time.deltaTime );
            PostProcsingControler.Current.chromaticAberration.intensity.value = Mathf.Lerp( PostProcsingControler.Current.chromaticAberration.intensity.value , 1 , 10 * Time.deltaTime );
            material.SetFloat(Speed1 , Mathf.Lerp(material.GetFloat(Speed1) , 0 , 10 * Time.deltaTime));
            material.SetFloat(Speed2 , Mathf.Lerp(material.GetFloat(Speed2) , 0 , 10 * Time.deltaTime));
            material.SetFloat(Speed3 , Mathf.Lerp(material.GetFloat(Speed3) , 0 , 10 * Time.deltaTime));
            material2.SetFloat(Speed4 , Mathf.Lerp(material2.GetFloat(Speed4) , 0 , 10 * Time.deltaTime));
            material2.SetFloat(Speed5 , Mathf.Lerp(material2.GetFloat(Speed5) , 0 , 10 * Time.deltaTime));
            if (wp < 0.5f)
            {
                PostProcsingControler.Current.lensDistortion.intensity.value = Mathf.Lerp( PostProcsingControler.Current.lensDistortion.intensity.value , -1 , wp * 2);
                return;
            }
            PostProcsingControler.Current.lensDistortion.intensity.value = Mathf.Lerp( PostProcsingControler.Current.lensDistortion.intensity.value , 0 , 7 * Time.deltaTime);
            return;
        }
        if (stop < 0)
            return;
        material.SetFloat(Speed1 , 0.25f);
        material.SetFloat(Speed2 , 0.02f);
        material.SetFloat(Speed3 , 0.1f );
        material2.SetFloat(Speed4 , 0.4f );
        material2.SetFloat(Speed5 , 1f );
        PostProcsingControler.Current.colorAdjustments.postExposure.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.postExposure.value , 0 , 10 * Time.deltaTime );
        PostProcsingControler.Current.colorAdjustments.hueShift.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.hueShift.value , 0 , 10 * Time.deltaTime );
        PostProcsingControler.Current.colorAdjustments.saturation.value = Mathf.Lerp( PostProcsingControler.Current.colorAdjustments.saturation.value , 0 , 10 * Time.deltaTime );
        PostProcsingControler.Current.chromaticAberration.intensity.value = Mathf.Lerp( PostProcsingControler.Current.chromaticAberration.intensity.value , 0 , 10 * Time.deltaTime );
        ZawardoTimeded = false;
    }
}
