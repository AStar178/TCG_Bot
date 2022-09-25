using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBirdTheBird : MonoBehaviour
{
    public static FireBirdTheBird Singeleton;
    [SerializeField] SpriteRenderer renderers;
    [SerializeField] GameObject FireBalls;
    public float ScalDamage;
    float attackSpeedCoolDown;
    Player player1;
    Transform Target;
    [SerializeField] float speed;

    public void StartAI(Player player)
    {
        player1 = player;
        Singeleton = this;
    }

    private void Update() 
    {
        if ( player1 == null )
            return;

        var yes = Physics2D.OverlapCircle( transform.position , 4 , player1.PlayerTarget.EnemyLayer );
        if (yes != null)
            Target = yes.transform;
        
        attackSpeedCoolDown -= Time.deltaTime;

        if ( attackSpeedCoolDown < 0 )
            ShootFireBall();

        renderers.flipX = player1.PlayerMoveMent.SpriteRenderer.flipX == true ? false : true;
    }

    private void ShootFireBall()
    {
        if (Target == null)
            return;

        attackSpeedCoolDown = player1.PlayerWeaponManger.AttackSpeed / 100;
        var fire = Instantiate ( FireBalls , transform.position , Quaternion.identity );
        fire.GetComponent<Rigidbody2D>().AddForce ( ( Target.position - transform.position ).normalized * speed );
        fire.GetComponent<PlayerBullent>().damage = Rpg.CreatDamage( Weapon().DamageAd * ScalDamage , Weapon().DamageAp * ScalDamage , Weapon().AmoroReduse , Weapon().MagicReduse , player1 , default , Target );
        fire.GetComponent<PlayerBullent>().EnemyLayer = 7;
        print ( "Shoot" );
        Destroy( fire , 10 );
    }
    private PlayerWeaponManger Weapon ()
    {
        return player1.PlayerWeaponManger;
    }
}
