using System;
using UnityEngine;

public class TrustyHoursePassive : AbilityPowerUps 
{
    public static TrustyHoursePassive Singiliton;
    [SerializeField] float SpeedScale;
    [SerializeField] float DangerSpeed;
    [SerializeField] Vector2 pos;
    [SerializeField] GameObject Hourse;
    SpriteRenderer spriteRenderer;
    float LastBuff;
    public override void OnPowerUp(Player player)
    {
        base.OnPowerUp(player);

        TrustyHoursePassive.Singiliton.SpeedScale += 0.1f;
        TrustyHoursePassive.Singiliton.DangerSpeed += 0.25f;
    }
    public override void OnHpChange()
    {
        GetPlayer().MoveSpeedBuff -= LastBuff;
        LastBuff = GetWHATHP();
        GetPlayer().MoveSpeedBuff += LastBuff;
    }

    private float GetWHATHP()
    {
        float persance = Rpg.HpMax( GetPlayer().PlayerHp.Currenthp , GetPlayer().PlayerHp.MaxHp );

        if (persance < 0.25f)
            return DangerSpeed;
        
        return SpeedScale;
    }
    public override void OnPowerUpUpdate()
    {
        if (spriteRenderer == null)
            return;
        
        spriteRenderer.flipX = GetPlayer().PlayerMoveMent.SpriteRenderer.flipX;
    }
    public override void OnFirstTime(Player player)
    {
        base.OnFirstTime(player);

        var h = Instantiate ( Hourse );
        LastBuff = GetWHATHP();
        GetPlayer().MoveSpeedBuff += LastBuff;
        h.transform.SetParent( GetPlayer().Body );
        h.transform.localPosition = pos;
        spriteRenderer = h.GetComponent<SpriteRenderer>();
        Singiliton = this;
    }



}