using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Threading.Tasks;
using System;

public class Nuking : IteamPassive
{

    [SerializeField] GameObject Effect; 
    [SerializeField] float CouldDown;
    [SerializeField] float DamageRaduis;
    [SerializeField] float Raduis;
    [SerializeField] float Delay = 1f;
    float coulddown;
    [SerializeField] float offset = 0.01f;
    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {

        NUKEENEMEYS(playerState);
        return state;
    }

    private async void NUKEENEMEYS(PlayerState playerState)
    {
        coulddown -= Time.deltaTime;
        if (coulddown > 0)
            return;
        
        
        var collider = Physics.OverlapSphere( PlayerPos(playerState) , Raduis , EnmeyLayer );
        var gays = collider.OrderBy( s => Vector3.Distance( PlayerPos(playerState) , s.transform.position ) ).ToList();
        var gay = collider.OrderBy( s => Vector3.Distance( PlayerPos(playerState) , s.transform.position ) ).FirstOrDefault();
        if (gay == null)
            return;
        coulddown = CouldDown;
        int current = level+1;
        for (int i = 0; i < (current); i++)
        {
            int xc = i;
            var enemy = xc >= gays.Count ? collider.OrderBy(s => UnityEngine.Random.value ).FirstOrDefault().transform: gays[xc].transform;
            Physics.Raycast( enemy.transform.position+ Vector3.up * 2 , Vector3.down , out var raycastHit , Mathf.Infinity , playerState.Player.PlayerThirdPersonController.GroundLayers );     
            var wow = Instantiate( Effect , (raycastHit.point + (Vector3.up * offset))  , Quaternion.identity );
            float wowx = Delay;
            while (wowx > 0)
            {
                wowx -= Time.deltaTime;
                Physics.Raycast( enemy.transform.position + Vector3.up * 2 , Vector3.down , out var raycastHitc , Mathf.Infinity , playerState.Player.PlayerThirdPersonController.GroundLayers );     
                wow.transform.position = raycastHitc.point + (Vector3.up * offset);
                await Task.Yield();
            }
            var colliders = Physics.OverlapSphere( wow.transform.position , DamageRaduis , EnmeyLayer );
            Destroy(wow , 6);
            for (int x = 0; x < colliders.Length; x++)
            {

                colliders[x].GetComponent<IDamageAble>().TakeDamage( CreatDamage( playerState.ResultValue.Damage * 3 , playerState , out var crited ) );
                
            }

        }

    }
}
