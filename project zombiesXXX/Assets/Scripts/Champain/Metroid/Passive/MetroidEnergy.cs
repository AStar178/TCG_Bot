using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetroidEnergy : MonoBehaviour
{
    public float EnergyMax = 100;
    public float Energy { get; private set; }

    public float CooloffSet = 4;
    private float Cooloff;

    public float EnergyRegen = 10;

    private float t;

    public Image[] EnergyImage;
    [SerializeField] Material material;
    [SerializeField] Material materiall;
    [SerializeField] Image EnergyImagess;

    public Color ImageColor;

    private float f;
    [SerializeField] float fadeOut;
    bool on;
    public void Start()
    {
        Energy = EnergyMax;
        Player.Current.UIManager.SetLeftBar(Energy, EnergyMax, ImageColor);
        f = 3;
    }

    public void DamageEnergy(float dam)
    {
        f = 0;
        Cooloff = CooloffSet;
        Energy -= dam;
        on = false;
        if (Energy < 0)
            Energy = 0;
        EnergyImagess.fillAmount = Mathf.Lerp(EnergyImagess.fillAmount, Energy / EnergyMax, 5 * Time.deltaTime);
        Player.Current.UIManager.SetLeftBar(Energy, EnergyMax, ImageColor);

        if (Energy <= 0)
        {
            RPGStatic.Instance.CreatCoustomTextPopup("NO ENERGY", transform.position, Color.yellow);
            Player.Current.UIManager.SetLeftBar(Energy, EnergyMax, ImageColor);
        }
    }

    public void RestoreEnergy(float dam)
    {
        f = 0;
        Energy += dam;
        on = false;
        if (Energy < 0)
            Energy = 0;
        EnergyImagess.fillAmount = Mathf.Lerp(EnergyImagess.fillAmount, Energy / EnergyMax, 5 * Time.deltaTime);
        Player.Current.UIManager.SetLeftBar(Energy, EnergyMax, ImageColor);

        if (Energy > EnergyMax)
        {
            Energy = EnergyMax;
            Player.Current.UIManager.SetLeftBar(Energy, EnergyMax, ImageColor);
        }

    }

    public void Update()
    {
        materiall.SetFloat("_Float", Mathf.Lerp( materiall.GetFloat("_Float") , Mathf.InverseLerp( 0 , EnergyMax , Energy ) , 4 * Time.deltaTime));
        if (Cooloff > 0)
        {
            Cooloff -= Time.deltaTime;
        } 
        else
        {
            t += Time.deltaTime;
            if (t >= .3f && Energy < EnergyMax)
            {
                f = 0;
                on = false;
                Energy += EnergyRegen;
                Player.Current.UIManager.SetLeftBar(Energy, EnergyMax, ImageColor);
                if (Energy > EnergyMax)
                { 
                    Energy = EnergyMax;
                    Player.Current.UIManager.SetLeftBar(Energy, EnergyMax, ImageColor);
                }
                t = 0;
            }

        }

        if (f < fadeOut)
            f += Time.deltaTime;
        else
        {
            on = true;
        }

        

        EnergyImagess.fillAmount = Mathf.Lerp(EnergyImagess.fillAmount, Energy / EnergyMax, 5 * Time.deltaTime);
        UIEFFECTDESPAE();
    }

    private void UIEFFECTDESPAE()
    {
        if (!on)
        {
            for (int i = 0; i < EnergyImage.Length; i++)
            {
                EnergyImage[i].color = Color.Lerp( EnergyImage[i].color , new Color( EnergyImage[i].color.r , EnergyImage[i].color.g , EnergyImage[i].color.b , 1 ) , 2 * Time.deltaTime );
            }
            material.SetColor("_Color" , Color.Lerp( material.GetColor("_Color") , new Color( material.GetColor("_Color").r , material.GetColor("_Color").g , material.GetColor("_Color").b , 1 ) , 2 * Time.deltaTime ));
            return;
        }
        for (int i = 0; i < EnergyImage.Length; i++)
        {
            EnergyImage[i].color = Color.Lerp( EnergyImage[i].color , new Color( EnergyImage[i].color.r , EnergyImage[i].color.g , EnergyImage[i].color.b , 0 ) , 5 * Time.deltaTime );
        }
        material.SetColor("_Color" , Color.Lerp( material.GetColor("_Color") , new Color( material.GetColor("_Color").r , material.GetColor("_Color").g , material.GetColor("_Color").b , 0 ) , 5 * Time.deltaTime ));
    }
}
