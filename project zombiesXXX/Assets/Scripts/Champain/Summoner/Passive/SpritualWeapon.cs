using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpritualWeapon : IteamPassive
{
    public bool Stop { get; private set; }

    [Header("Clean")]

    [SerializeField]
    private SpiritBox Spiritual;

    [SerializeField]
    private Rigidbody rb;

    [SerializeField]
    private float ShootDam;

    [SerializeField]
    private float Range;

    [SerializeField]
    private ParticleSystem bullet;
    private ParticleSystem bulleteffecrt;

    private Transform target;

    [SerializeField]
    private float ShotCooldownSet;
    private float ShotCooldown;

    [SerializeField]
    private float BulletSpeed;

    private SummonerEffects SummonerEffects;

    [SerializeField]
    private int SoulNeded;
    [SerializeField]
    private int SoulNededAdd;
    private int Souls;

    [Header("Charge")]

    private bool inCharge;

    [SerializeField]
    private float ChargeDam;

    [SerializeField]
    private EnemyHp ChargeTarget;

    [SerializeField]
    private float ChargeSpeed;

    [Header("Vortex")]

    [SerializeField]
    private float VortTimeSet;
    private float VortTime;

    [SerializeField]
    private int VortDamPerTickSet;
    private int VortDamPerTick;

    [SerializeField]
    private float VortDam;

    [SerializeField]
    private float VortRange;

    [SerializeField]
    private int VortApplyPerTicksSet;
    private int VortApplyPerTicks;

    [SerializeField]
    private float VortForce;

    private List<EnemyHp> VortTargets;
    private float TickChecker;
    private Vector3 VortPoint;

    public bool StartChargeAtTarget()
    {
        if (target != null)
        {
            ChargeTarget = target.GetComponent<EnemyHp>();
            Spiritual.ActiveSword();
            return true;
        }
        return false;
    }

    public override void OnStart(PlayerState playerState)
    {

        SummonerEffects = playerState.GetComponent<SummonerEffects>();
        SummonerEffects.SetPassive(this);

        Spiritual = Instantiate(Spiritual, transform);
        Spiritual.transform.position = SummonerEffects.SpiritualPos().position;
        Spiritual.Bulletref.GetComponent<ParticalClide>().unityEvent.AddListener(OnDealDamage);
        bullet = Spiritual.Bulletref;
        bulleteffecrt = Spiritual.Bulletrefd;
        rb = Spiritual.GetComponent<Rigidbody>();
        playerState.OnKilledEnemy += Absorb;
        level++;

        Spiritual.ActiveOrb();

        base.OnStart(playerState);
    }

    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        if (VortPoint != Vector3.zero)
        {
            Vortex(playerState);
        }

        if (ChargeTarget != null)
            Charge(playerState);
        else if (ChargeTarget == null && Stop && inCharge)
        {
            rb.velocity = Vector3.zero; Stop = false; inCharge = false;
            Spiritual.ActiveOrb();
            SummonerEffects.S1.Failed();
        }

        if (!Stop)
        {
            rb.velocity = Vector3.zero;
            ShootUpdate(playerState);
        }

        return base.OnUpdate(playerState, ref CalucatedValue, ref state);
    }

    public void ShootUpdate(PlayerState playerState)
    {
        if (Stop == true)
            return;

        Spiritual.transform.position = Vector3.Lerp(Spiritual.transform.position, SummonerEffects.SpiritualPos().position, 5 * Time.deltaTime);

        if (playerState.Player.PlayerTargetSystem.Target != null)
            target = playerState.Player.PlayerTargetSystem.Target.transform;
        else
            target = FindTarget(playerState.ResultValue.AttackRange, playerState.transform, target);

        if (target != null)
            Shoot(playerState);
        else Spiritual.transform.rotation = playerState.transform.rotation;
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
            float b = (level) / 4;
            if (b > 10)
                b = 10;
            else if (b < 1)
                b = 1;
            ShotCooldown = ShotCooldownSet / b;
        } else ShotCooldown -= Time.deltaTime;
    }

    private float t;
    public void Charge(PlayerState playerState)
    {
        Stop = true;
        inCharge = true;

        rb.velocity = (ChargeTarget.transform.position - Spiritual.transform.position).normalized * ChargeSpeed;
        LookAt(ChargeTarget.transform);

        if (Vector3.Distance(ChargeTarget.transform.position, Spiritual.transform.position) <= .5f)
        {
            DamageData dam = CreatDamage(PlayerState.ResultValue.Damage + (ChargeDam * level / 4f), PlayerState, out var crited);
            ChargeTarget.TakeDamage(dam);


            Stop = false;
            PlayerState.OnAbilityAttackDealDamage?.Invoke(dam, ChargeTarget);
            ChargeTarget = null;
            rb.velocity = Vector3.zero;
            Spiritual.ActiveOrb();
            InCombat();
            t = 0;
        }
    }

    public void StartVortex(Vector3 point)
    {
        VortPoint = point;
        VortTime = VortTimeSet;
    }

    public void Vortex(PlayerState playerState)
    {
        if (VortPoint == Vector3.zero)
            return;

        Stop = true;
        rb.velocity = (VortPoint - Spiritual.transform.position).normalized * ChargeSpeed;
        if (Vector3.Distance(VortPoint, Spiritual.transform.position) < .5f)
        {
            rb.velocity = Vector3.zero;
            Spiritual.transform.position = VortPoint;
            Vorting(playerState);

            if (VortTime > 0)
            {
                VortTime -= Time.deltaTime;
            }

            if (VortTime <= 0)
            {
                VortPoint = Vector3.zero;

                Stop = false;

                foreach (EnemyHp enemyHp in VortTargets)
                {
                    if (enemyHp != null)
                        enemyHp.GetComponent<Rigidbody>().velocity = Vector3.zero;
                }
            }
        }
    }

    public void Vorting(PlayerState playerState)
    {
        TickChecker += Time.deltaTime;
        InCombat();
        if (TickChecker >= 0.1f)
        {
            TickChecker = 0;
            VortApplyPerTicks--;
            VortDamPerTick--;
        }

        var _targets = Physics.OverlapSphere(Spiritual.transform.position, VortRange, PlayerState.Player.Enemy);
        List<EnemyHp> targetsHp = new List<EnemyHp>();

        foreach (var target in _targets)
        {
            targetsHp.Add(target.GetComponent<EnemyHp>());
        }

        VortTargets = targetsHp;

        if (VortDamPerTick <= 0)
        {
            VortDamPerTick = VortDamPerTickSet;
            foreach (EnemyHp enemyHp in VortTargets)
            {
                float b = (level) / 20;
                if (b > 2)
                    b = 2;
                else if (b < .1f)
                    b = .1f;

                DamageData data = CreatDamage(VortDam + playerState.ResultValue.Damage * (level * b), playerState, out bool a);

                if (VortApplyPerTicks <= 0)
                {
                    if (enemyHp != null)
                        playerState.OnAbilityAttackDealDamage?.Invoke(data, enemyHp);
                }

                if (enemyHp != null)
                    enemyHp.TakeDamage(data);

            }
        }

        if (VortApplyPerTicks <= 0)
            VortApplyPerTicks = VortApplyPerTicksSet;

        float c = (level) / 4;
        if (c > 10)
            c = 10;
        else if (c < 1)
            c = 1;

        foreach (EnemyHp enemyHp in VortTargets)
        {
            enemyHp.transform.position = Vector3.Lerp(enemyHp.transform.position, Spiritual.transform.position, (VortForce * c) * Time.deltaTime);
        }

    }

    private void OnDealDamage(EnemyHp targ)
    {
        DamageData d = CreatDamage(PlayerState.ResultValue.Damage + (ShootDam * level / 8), PlayerState, out var crited);
        targ.TakeDamage(d);


        PlayerState.OnAbilityAttackDealDamage?.Invoke(d, targ);
    }

    public void Absorb( DamageData data, EnemyHp enemyHp)
    {
        Souls++;
        if (Souls >= SoulNeded)
        {
            Souls = 0;
            SoulNeded += SoulNededAdd;
            level += 2 + SoulNeded / 70;
        }
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
