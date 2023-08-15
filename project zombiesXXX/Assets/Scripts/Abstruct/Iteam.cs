using UnityEngine;
using System.Linq;

public abstract class Iteam : MonoBehaviour {
    public int Oderlayer = 0;
    public virtual void OnStart(PlayerState playerState)
    {

    }
    public virtual State OnUpdate(PlayerState playerState , ref State CalucatedValue , ref State state)
    {
        return state;
    }

    public virtual void OnDrop(PlayerState playerState)
    {

    }

    public virtual void OnLevelUp(PlayerState playerState)
    {

    }

    public DamageData CreatDamage(float damage , PlayerState playerState , out bool crited)
    {
        crited = false;
        DamageData damageData = new DamageData();
        damageData.DamageAmount = damage;
        if (Random.value <= playerState.ResultValue.Crit)
        {
            damageData.DamageAmount *= playerState.ResultValue.CritDamageMulty == 0 ? 1 : playerState.ResultValue.CritDamageMulty;
            crited = true;
        }
        damageData.target = playerState;
        damageData.Crited = crited;
        return damageData;
    }
    public DamageData CreatDamageWithOutCrit(float damage , PlayerState playerState)
    {
        DamageData damageData = new DamageData();
        damageData.DamageAmount = damage;
        damageData.target = playerState;
        return damageData;
    }
    
    public Vector3 PlayerPos(PlayerState playerState)
    {
        return playerState.Player.PlayerState.transform.position;
    }
    public Vector3 PlayerGetpos => Player.Current.PlayerState.transform.position;
    
    public GameObject PlayerGameObject(PlayerState playerState)
    {
        return playerState.Player.PlayerState.gameObject;
    }
    public PlayerState PlayerState => Player.Current.PlayerState;

    
    public LayerMask EnmeyLayer => Player.Current.Enemy;
    public LayerMask GroundLayer => Player.Current.PlayerThirdPersonController.GroundLayers;
    protected void InCombat()
    {
        Player.Current.PlayerState.Combat = true;
        Player.Current.PlayerState.xc = 5;
    }
    protected RaycastHit raycastHit => Player.Current.PlayerState.RaycastHitHit;
    protected bool DistanceCheakPlayerCameraRayCast(float Range)
    {
        if (Player.Current.PlayerState.RaycastHitHit.collider == null)
            return false;
        if (Vector3.Distance(Player.Current.CameraControler.transform.position, Player.Current.PlayerState.RaycastHitHit.point) < Range)
            return true;
        return false;

    }
    public float GetAttackSpeed()
    {
        return (1/(Player.Current.PlayerState.ResultValue.AttackSpeed+1));
    }
    protected bool DistanceCheakPlayerRayCast(float Range)
    {
        if (Player.Current.PlayerState.RaycastHitHit.collider == null)
            return false;
        if (Vector3.Distance(Player.Current.PlayerState.transform.position, Player.Current.PlayerState.RaycastHitHit.point) < Range)
            return true;
        return false;

    }
}