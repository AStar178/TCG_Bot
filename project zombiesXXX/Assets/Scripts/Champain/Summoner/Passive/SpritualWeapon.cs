using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.VFX;

public class SpritualWeapon : IteamPassive
{
    public bool Stop { get; private set; }

    #region Shoot
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
    #endregion

    [SerializeField]
    private int SoulNeded;
    [SerializeField]
    private int SoulNededAdd;
    private int Souls;

    #region Charge
    [Header("Charge")]

    [SerializeField]
    private float ChargeDam;

    [SerializeField]
    private float ChargeExpRadius;

    [SerializeField]
    private float ChargeExpMulty;

    [SerializeField]
    private EnemyHp ChargeTarget;

    [SerializeField]
    private float ChargeSpeed;

    private bool inCharge;
    #endregion

    #region Vort
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
    #endregion

    #region Beam (Without Beams :)
    [Header("Beam")]

    [SerializeField]
    private Vector3 BeamOffset;
    private Vector3 BeamNewVal;

    [SerializeField]
    private float BeamAtkRange;
    [SerializeField]
    private float BeamAtkRadius;

    [SerializeField]
    private float BeamDam = 2f;

    [SerializeField]
    private float BeamTimeSet = 3f;
    private float BeamTime;

    [SerializeField]
    private float BeamAtkDelaySet = 3f;
    private float BeamAtkDelay;

    private bool Beaming;

    #endregion

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

        if (Beaming == true)
            BeamUpdate(playerState);

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

    #region Shoot
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

    private void OnDealDamage(EnemyHp targ)
    {
        DamageData d = CreatDamage(PlayerState.ResultValue.Damage + (ShootDam * level / 8), PlayerState, out var crited);
        targ.TakeDamage(d);


        PlayerState.OnAbilityAttackDealDamage?.Invoke(d, targ);
    }
    #endregion

    #region Charge
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

            var _target = Physics.OverlapSphere(Spiritual.transform.position, ChargeExpRadius, PlayerState.Player.Enemy);
            var a = new List<EnemyHp>();

            foreach (Collider collider in _target)
                a.Add(collider.gameObject.GetComponent<EnemyHp>());

            a.Remove(ChargeTarget);

            Spiritual.SpawnExplosive(ChargeTarget.transform.position);

            dam.DamageAmount *= ChargeExpMulty;

            foreach (EnemyHp hp in a)
                hp.TakeDamage(dam);

            Stop = false;
            PlayerState.OnAtuoAttackDealDamage?.Invoke(dam, ChargeTarget);

            ChargeTarget = null;
            rb.velocity = Vector3.zero;
            Spiritual.ActiveOrb();
        }
    }
    #endregion

    #region Vortex
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
        Spiritual.ActiveBlackHole(true);
        if (Vector3.Distance(VortPoint, Spiritual.transform.position) < .5f)
        {
            rb.velocity = Vector3.zero;
            Spiritual.transform.position = VortPoint;
            Vorting(playerState);

            if (VortTime > 0)
            {
                VortTime -= Time.deltaTime;
                Spiritual.transform.rotation = Quaternion.identity;
            }

            if (VortTime <= 0)
            {
                VortPoint = Vector3.zero;

                Stop = false;

                Spiritual.ActiveBlackHole(false);
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
    #endregion

    #region Explosion
    public void StartBeam()
    {
        Beaming = true;
        BeamTime = BeamTimeSet;
    }

    public void BeamUpdate(PlayerState playerState)
    {
        if (BeamTime > 0)
        {
            BeamTime -= Time.deltaTime;

            Spiritual.transform.position = Vector3.Lerp(Spiritual.transform.position, BeamOffset + playerState.transform.position, 5 * Time.deltaTime);

            if (BeamAtkDelay <= 0)
            {
                Stop = true;

                BeamAtkDelay = BeamAtkDelaySet;

                var a = level / 10;
                if (a > 10)
                    a = 10;
                else if (a < 1)
                    a = 1;

                var dam = BeamDam + (BeamDam * a);

                DamageData damen = CreatDamage(dam, playerState, out bool c);

                var _target = Physics.OverlapSphere(playerState.transform.position, BeamAtkRange, PlayerState.Player.Enemy);

                for (int i = 0; i < 3; i++)
                {
                    if (_target.Length <= 0)
                        break;

                    Transform tar = _target[UnityEngine.Random.Range(0, _target.Length)].transform;

                    Spiritual.SpawnExplosive(tar.position);
                    var tarDam = Physics.OverlapSphere(tar.position, BeamAtkRadius, PlayerState.Player.Enemy);

                    foreach (Collider collider in tarDam)
                    {
                        collider.GetComponent<EnemyHp>().TakeDamage(damen);
                    }
                }
            }
            else
                BeamAtkDelay -= Time.deltaTime;
        }
        else
        {
            Stop = false;
            Beaming = false;
        }

    }
    #endregion

    public void Absorb( DamageData data, EnemyHp enemyHp)
    {
        Souls++;
        if (Souls >= SoulNeded)
        {
            Souls -= SoulNeded;
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
