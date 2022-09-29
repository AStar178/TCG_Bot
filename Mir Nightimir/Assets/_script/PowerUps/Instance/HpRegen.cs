using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpRegen : MonoBehaviour
{
    public static HpRegen Singeleton;
    public Vector3 pos;
    [SerializeField] SpriteRenderer renderers;
    float Heal = .5f;
    [SerializeField] Player master;
    float HealTimer;
    public float TimeToHeal = 3;

    public void StartAI(Player player)
    {
        master = player;
        Singeleton = this;
        renderers = gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update() 
    {
        if ( master == null )
            return;
        if (HealTimer >= TimeToHeal)
        {
            if (master.PlayerHp.Currenthp > master.PlayerHp.MaxHp)
            {
                master.PlayerHp.Currenthp = master.PlayerHp.MaxHp;
            }
            else if (master.PlayerHp.Currenthp < master.PlayerHp.MaxHp)
            {
                master.PlayerHp.Currenthp += master.PlayerHp.Currenthp + Heal;
                if (master.PlayerHp.Currenthp > master.PlayerHp.MaxHp)
                {
                    master.PlayerHp.Currenthp = master.PlayerHp.MaxHp;
                }
            }
            HealTimer = 0;
        }

        transform.localPosition = pos;

        HealTimer += Time.deltaTime;

        if (master.PlayerWeaponManger.CurrentWeapons.rotationLeftSprite == false)
        {
            renderers.flipX = master.PlayerMoveMent.SpriteRenderer.flipX == true ? false : true;
        }
        else 
        {
            renderers.flipX = master.PlayerMoveMent.SpriteRenderer.flipX == true ? true : false;
        }
    }
}
