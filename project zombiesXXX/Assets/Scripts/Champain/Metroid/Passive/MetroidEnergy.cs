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

    public void Start()
    {
        Energy = EnergyMax;
    }

    public void DamageEnergy(float dam)
    {
        Cooloff = CooloffSet;
        Energy -= dam;
        if (Energy < 0)
            Energy = 0;
        UpdateImage();
        Debug.Log($"Took {dam} energy and we have {Energy} energy left!");
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
                Energy += EnergyRegen;
                if (Energy > EnergyMax)
                { Energy = EnergyMax; }
                Debug.Log($"we have {Energy} energy");
                UpdateImage();
                t = 0;
            }
        }
    }

    public void UpdateImage()
    {
        EnergyImage.fillAmount = Energy / EnergyMax;
    }
}
