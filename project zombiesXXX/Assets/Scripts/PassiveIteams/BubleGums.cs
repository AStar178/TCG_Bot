using System;
using UnityEngine;

public class BubleGums : IteamSkill 
{
    [SerializeField] float coldDown;
    [SerializeField] GameObject bubble;
    [SerializeField] GameObject bubblepartical;
    [SerializeField] float force;
    [SerializeField] float forcexc;
    [SerializeField] float BubleGumsTIMER;
    [SerializeField] float AttackSpeed;
    [SerializeField] Vector3 Offset;
    [SerializeField] GameObject CurrentBubble;
    [SerializeField] Rigidbody enemyRigidBody;
    float x;
    float t;
    float xc;
    public override void OnUseSkill(PlayerState playerState)
    {

        if ( x > 0 )
            return;
        
        CurrentBubble = Instantiate( bubble , playerState.transform.position , Quaternion.identity );
        x = coldDown;
        t = BubleGumsTIMER;

        base.OnUseSkill(playerState);
    }
    private void Update() {
        x -= Time.deltaTime;
        t -= Time.deltaTime;
        xc -= Time.deltaTime;
        if ( Player.Current.PlayerTargetSystem.Target == null)
        {
            if ( CurrentBubble == null )
                return;
            BUBBLETHEPLAYER();
            return;
        }
            
        
        if ( CurrentBubble == null )
            return;
        


        CurrentBubble.transform.position = 
        Vector3.Lerp
            ( 
                CurrentBubble.transform.position 
                ,Player.Current.PlayerTargetSystem.Target.transform.position 
                ,10 * Time.deltaTime 
            );

        if ( Vector3.Distance( CurrentBubble.transform.position , Player.Current.PlayerTargetSystem.Target.transform.position ) < 1f )
        {

            if ( t < 0 )
            {
                var s = Instantiate(bubblepartical , CurrentBubble.transform.position , Quaternion.identity );
                Destroy(CurrentBubble);
                Destroy(s , 2);
            }

            
            enemyRigidBody = Player.Current.PlayerTargetSystem.Target.GetComponent<Rigidbody>();
            enemyRigidBody.velocity = ( Vector3.up * force );

            if (xc < 0)
            {
                var s = CreatDamageWithOutCrit( Player.Current.PlayerState.ResultValue.Damage * 0.1f , Player.Current.PlayerState );
                enemyRigidBody.GetComponent<IDamageAble>().TakeDamage( s );
                xc = AttackSpeed;
            }

        }
    }

    private void BUBBLETHEPLAYER()
    {
        CurrentBubble.transform.position = 
        Vector3.Lerp
            ( 
                CurrentBubble.transform.position 
                ,Player.Current.PlayerTargetSystem.transform.position + Offset 
                ,10 * Time.deltaTime 
            );
        
        Player.Current.PlayerState.ResultValue.Deffece *= 2;
        Player.Current.PlayerThirdPersonController.rb.AddForce( Vector3.up * forcexc , ForceMode.Force );
    }
}