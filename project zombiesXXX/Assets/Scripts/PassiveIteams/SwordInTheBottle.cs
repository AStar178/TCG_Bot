using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UnityEngine;

public class SwordInTheBottle : IteamPassive
{
    public float RotateSpeed;
    public float Damage;
    public float RangeOffset;
    public float RotationSpeed;
    public Vector3 StandOffset;
    public float AggroRange;
    public float speed;
    public float AggroSet;
    public List<SwordHelper> swordHelpers;
    public int MaxLevelScaling;
    public bool OnAbilityTrigger = false;
    public bool OnAttackTrigger = false;
    public bool IncreaseOnLvl = true;
    public float NumberOfSwords = 1;
    public float DamageScaling = .1f;
    [SerializeField] GameObject jessy;
    public Transform target;
    public LayerMask Enemy;

    public GameObject SwordPrefab;

    public override void OnStart(PlayerState playerState)
    {
        for (int i = 0; i < NumberOfSwords; i++)
            swordHelpers.Add(Instantiate(SwordPrefab.GetComponent<SwordHelper>(), transform));

        transform.localPosition = Vector3.zero;
        base.OnStart(playerState);
    }

    public override void OnLevelUp(PlayerState playerState)
    {
        if (IncreaseOnLvl)
            swordHelpers.Add(Instantiate(SwordPrefab.GetComponent<SwordHelper>(), transform));

        base.OnLevelUp(playerState);
    }

    public override State OnUpdate(PlayerState playerState, ref State CalucatedValue, ref State state)
    {
        for (int i = 0; i < swordHelpers.Count; i++)
        {
            SwordThings(playerState, swordHelpers[i].transform, swordHelpers[i], i);
        }
            return base.OnUpdate(playerState, ref CalucatedValue, ref state);
    }

    private float angle;
    public void SwordThings(PlayerState playerState, Transform sword, SwordHelper swordHelper, int i)
    {
        if (playerState.Player.PlayerTargetSystem.Target == null)
            target = FindTarget(AggroRange, playerState.transform, target);
        else target = playerState.Player.PlayerTargetSystem.Target.transform;

        if (swordHelper.Aggro > 0)
            swordHelper.Aggro -= Time.deltaTime;

        if (target != null)
        {
            Vector3 dir = (target.position - sword.position).normalized;

            Quaternion wow = Quaternion.LookRotation(dir);

            if (swordHelper.Aggro > 0)
                sword.localRotation = Quaternion.Lerp(sword.localRotation, wow, RotateSpeed * Time.deltaTime);
            else
            { sword.localRotation = wow; }

            if (Vector3.Distance(sword.position, target.position) < .5f && swordHelper.Aggro <= 0)
            {
                DamageData damageData = CreatDamage((Damage * Scaling1and0(MaxLevelScaling)) + (playerState.ResultValue.Damage * DamageScaling), playerState, out var a);
                target.GetComponent<EnemyHp>().TakeDamage(damageData);
                EnemyHp enemyHp = target.GetComponent<EnemyHp>();

                if (OnAttackTrigger)
                    playerState.OnAtuoAttackDealDamage?.Invoke(damageData, enemyHp);
                if (OnAbilityTrigger)
                {
                    playerState.OnAbilityAttackDealDamage?.Invoke(damageData, enemyHp);
                    JESUSETHEENEMY(damageData , enemyHp);
                }
                    

                swordHelper.Aggro = AggroSet;
            }

            swordHelper.rb.velocity = sword.forward * speed;
        }
        else
        {
            float f =  (float)i/swordHelpers.Count;
            float x = Mathf.Cos( angle + (f * (Mathf.PI * 2))) * RangeOffset;
            float z = Mathf.Sin(angle + (f * (Mathf.PI * 2))) * RangeOffset;

            swordHelper.rb.velocity = Vector3.zero;
            sword.position = Vector3.Lerp(sword.position, playerState.transform.position + StandOffset + new Vector3(x, 0, z), speed * Time.deltaTime);

            sword.localEulerAngles = new Vector3(90, playerState.transform.localEulerAngles.y, 0);
            angle += Time.deltaTime * RotationSpeed;
        }
    }

    private async void JESUSETHEENEMY(DamageData damageData, EnemyHp enemyHp)
    {
        float x = level == 0 ? 1 : level;
        for (int i = 0; i < level * level; i++)
        {
            if (enemyHp == null)
                return;
            var s = Instantiate( jessy , enemyHp.transform.position + (1.25f * (PlayerGetpos - enemyHp.transform.position).normalized)  + Vector3.up * 0.5f, Quaternion.identity );
            enemyHp.TakeDamage(damageData);
            Destroy(s , 2);
            PlayerState.OnAtuoAttackDealDamage?.Invoke(damageData, enemyHp);
            await Task.Delay( (int)((1f/(level * level)) * 1000) );

        }
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
