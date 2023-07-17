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

    public Image EnergyImage;

    private float f;
    [SerializeField] float fadeOut;

    public void Start()
    {
        Energy = EnergyMax;
        f = 3;
    }

    public void DamageEnergy(float dam)
    {
        EnergyImage.gameObject.SetActive(true);
        f = 0;
        Cooloff = CooloffSet;
        Energy -= dam;
        if (Energy < 0)
            Energy = 0;
    }

    public void Update()
    {
        if (Cooloff > 0)
            Cooloff -= Time.deltaTime;
        else
        {
            t += Time.deltaTime;
            if (t >= .3f && Energy < EnergyMax)
            {
                f = 0;
                EnergyImage.gameObject.SetActive(true);
                Energy += EnergyRegen;
                if (Energy > EnergyMax)
                { 
                    Energy = EnergyMax; 
                }
                t = 0;
            }

        }

        if (f < fadeOut)
            f += Time.deltaTime;
        else EnergyImage.gameObject.SetActive(false);

        EnergyImage.fillAmount = Mathf.Lerp(EnergyImage.fillAmount, Energy / EnergyMax, 0.1f);
    }

}
