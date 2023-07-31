using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Nurrets : IteamPassive
{
    [Header("Iteam")]
    public Transform Target;
    public float AttackCooldownSet;
    private float AttackCooldown;
    public float AttackDamage, AttackRange;
    public GameObject ImpactEffect;
    public LayerMask Enemy;


    [Header("Circle")]

    // Drag & drop the player in the inspector

    public float RotationSpeed = 1;

    public float CircleRadius = 1;

    public float ElevationOffset = 0;

    private Vector3 positionOffset;

    public float Speed;

    private float angle;

    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        Circling(playerState.transform);

        Attack(playerState);

        return base.OnUpdate(playerState, ref CalucatedValue, ref state);
    }

    public void Circling(Transform player)
    {
        positionOffset.Set(
            Mathf.Cos(angle) * CircleRadius,
            ElevationOffset,
            Mathf.Sin(angle) * CircleRadius
        );
        transform.position = player.position + positionOffset;
        angle += Time.deltaTime * RotationSpeed;
    }

    public void Attack(PlayerState playerState)
    {
        if (playerState.Player.PlayerTargetSystem.Target != null)
            Target = playerState.Player.PlayerTargetSystem.Target.transform;
        else
            Target = FindTarget(AttackRange, playerState.transform, Target);

        if (Target != null)
        {
            transform.LookAt(Target);

            if (AttackCooldown <= 0)
            {
                DamageData dam = CreatDamage(AttackDamage, playerState, out var a);
                EnemyHp enemyHp = Target.GetComponent<EnemyHp>();
                enemyHp.TakeDamage(dam);
                playerState.OnAtuoAttackDealDamage?.Invoke(dam, enemyHp);
                Transform eff = Instantiate(ImpactEffect).transform;
                eff.position = transform.position;
                eff.localRotation = transform.localRotation;
                Destroy(eff.gameObject, 1);
                AttackCooldown = AttackCooldownSet * Scaling1and0();
            } else
            {
                AttackCooldown -= Time.deltaTime;
            }
        }
        else
            transform.localRotation = Quaternion.identity;
    }

    public Transform FindTarget(float AttackRange, Transform position, Transform SelectedTarget)
    {
        var _target = Physics.OverlapSphere(position.position, AttackRange, Enemy);
        var _sad = _target.OrderBy(n => (position.position - n.transform.position).magnitude).Where(n => n != SelectedTarget).FirstOrDefault();
        if (_sad == null)
        {
            return null;
        }

        return _sad.transform;
    }
}
