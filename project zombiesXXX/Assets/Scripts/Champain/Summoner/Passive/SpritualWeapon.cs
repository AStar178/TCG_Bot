using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpritualWeapon : IteamPassive
{
    [Header("Clean")]

    [SerializeField]
    private SpiritBox Spiritual;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float Dam;

    [SerializeField]
    private float Range;

    [SerializeField]
    private ParticleSystem bullet;
    private ParticleSystem bulleteffecrt;

    [SerializeField]
    private Vector3 Offset;

    private Transform target;

    [SerializeField]
    private float ShotCooldownSet;
    private float ShotCooldown;

    [SerializeField]
    private float BulletSpeed;

    [Header("Charge")]

    [SerializeField]
    private EnemyHp ChargeTarget;

    [SerializeField]
    private float ChargeSpeed;

    public void StartChargeAtTarget(EnemyHp target)
    {
        ChargeTarget = target;
        Spiritual.ActiveSword();
    }

    public override void OnStart(PlayerState playerState)
    {
        Spiritual = Instantiate(Spiritual, transform);
        Spiritual.transform.position = playerState.transform.position + Offset;
        Spiritual.Bulletref.GetComponent<ParticalClide>().unityEvent.AddListener(OnDealDamage);
        bullet = Spiritual.Bulletref;
        bulleteffecrt = Spiritual.Bulletrefd;
        rb = Spiritual.GetComponent<Rigidbody>();

        Spiritual.ActiveOrb();

        base.OnStart(playerState);
    }

    private void OnDealDamage(EnemyHp arg0)
    {
        var d = CreatDamage(PlayerState.ResultValue.Damage , PlayerState , out var crited);
        arg0.TakeDamage(d);


        PlayerState.OnAbilityAttackDealDamage?.Invoke(d , arg0);

    }

    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {

        if (ChargeTarget != null)
            Charge(playerState);
        else
        {
            Spiritual.transform.position = Vector3.Lerp(Spiritual.transform.position, playerState.transform.position + Offset, 5 * Time.deltaTime);

            if (playerState.Player.PlayerTargetSystem.Target != null)
                target = playerState.Player.PlayerTargetSystem.Target.transform;
            else
                target = FindTarget(playerState.ResultValue.AttackRange, playerState.transform, target);

            if (target != null)
                Shoot(playerState);
        }

        return base.OnUpdate(playerState, ref CalucatedValue, ref state);
    }

    private void Shoot(PlayerState playerState)
    {
        LookAt(target);
        
        bullet.transform.position = target.transform.position;
        var shape = bullet.shape;
        shape.position = bullet.transform.InverseTransformPoint(Spiritual.transform.position);
        if (ShotCooldown <= 0)
        {
            bullet.Play();
            bulleteffecrt.Play();
            ShotCooldown = ShotCooldownSet;
        } else ShotCooldown -= Time.deltaTime;
    }

    public void Charge(PlayerState playerState)
    {

    }

    public void LookAt(Transform target)
    {
        Vector3 dir = (target.position - Spiritual.transform.position).normalized;

        Spiritual.transform.rotation = Quaternion.LookRotation(dir);
    }

    public Transform FindTarget(float AttackRange, Transform position, Transform SelectedTarget)
    {
        var _target = Physics.OverlapSphere(position.position, AttackRange, PlayerState.Player.Enemy);
        var _sad = _target.OrderBy(n => (position.position - n.transform.position).magnitude).Where(n => n != SelectedTarget).FirstOrDefault();
        if (_sad == null)
        {
            return null;
        }

        return _sad.transform;
    }
}
