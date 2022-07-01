using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class TripleAttack : Ability
{
    public virtual void PAttack(BattleS battle)
    {
        PlayerAttack(battle);
    }

    async void PlayerAttack(BattleS battle)
    {
        Transform target = battle.EnemyKnight.gameObject.transform;
        Transform Attacker = battle.PlayerKnight.gameObject.transform;
        int atk = battle.PlayerAtk;

        Tween tween = battle.PlayerKnight.gameObject.transform.DOMoveX(target.position.x, Util.Setting.Speed * 0.25f);
        battle.PlayerKnight.gameObject.transform.DORotate(new Vector3(0,0,-16), Util.Setting.Speed);
        await tween.AsyncWaitForCompletion();
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(target.position.x + 1, Util.Setting.Speed * 0.125f);
        target.transform.DOMoveY(target.position.y + 2, Util.Setting.Speed * 0.25f);
        await tween.AsyncWaitForCompletion();
        TokeDamage(battle, atk - 2, target);
        await Task.Delay(12);
        battle.PlayerKnight.flipX = true;
        battle.PlayerKnight.gameObject.transform.DORotate(new Vector3(0, 0, 16), Util.Setting.Speed);
        tween = Attacker.DOMoveX(target.position.x - 1, Util.Setting.Speed * 0.125f);
        tween = Attacker.DOMoveY(target.position.y, Util.Setting.Speed * 0.125f);
        TokeDamage(battle, atk - 2, target);
        await tween.AsyncWaitForCompletion();

        battle.PlayerKnight.flipX = false;
        tween = target.transform.DOMoveY(target.position.y + 2, Util.Setting.Speed * 0.125f);
        Attacker.DOMoveX(target.position.x + 1, Util.Setting.Speed * 0.125f);
        Attacker.DORotate(new Vector3(0, 0, -16), Util.Setting.Speed);
        tween = Attacker.DOMoveY(target.position.y + 2, Util.Setting.Speed * 0.125f);
        await tween.AsyncWaitForCompletion();
        battle.PlayerKnight.flipX = true;

        Attacker.DOMove(new Vector3(target.position.x, target.position.y + 1, target.position.z), Util.Setting.Speed * .125f);
        tween = Attacker.DORotate(new(0, 0, 16), Util.Setting.Speed * .125f);
        await tween.AsyncWaitForCompletion();
        tween = target.DOJump(new Vector3(5.5f, -3.5f, 0), 0.5f, 1, Util.Setting.Speed * 0.125f);
        Attacker.DOJump(new Vector3(1.5f, -3.5f, 0), 0.5f, 1, Util.Setting.Speed * 0.125f);
        await tween.AsyncWaitForCompletion();
        TokeDamage(battle, atk + Random.Range(1,6), target);
        battle.PlayerKnight.flipX = false;
        tween = Attacker.DOMoveX(-5.5f, Util.Setting.Speed * .25f);
        Attacker.DORotate(new Vector3(0, 0, 0), Util.Setting.Speed);
        await tween.AsyncWaitForCompletion();
        tween.Kill();
    }

    async void TokeDamage(BattleS battle, int atk, Transform target)
    {
        battle.DamageTarget(target.gameObject , atk);
        battle.changAttribiutText();
        battle.changeColor(Color.red, battle.EnemyKnight);
        battle.EnemyKnight.transform.DOShakePosition(.09f);
        await Task.Delay(12);
        battle.changeColor(Color.white, battle.EnemyKnight);
    }

    public virtual void EAttack(BattleS battle)
    {
        EnemyAttack(battle);
    }

    async void EnemyAttack(BattleS battle)
    {
        Tween tween = battle.EnemyKnight.gameObject.transform.DOMoveX(battle.transform.position.x - 4, Util.Setting.Speed);
        await tween.AsyncWaitForCompletion();
        battle.PlayerDef -= battle.EnemyAtk;
        battle.changAttribiutText();
        battle.changeColor(Color.red, battle.PlayerKnight);
        battle.EnemyKnight.transform.DOShakePosition(.1f);
        await Task.Delay(10);
        await tween.AsyncWaitForCompletion();
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(5.5f, Util.Setting.Speed);
        battle.changeColor(Color.white, battle.PlayerKnight);
        await tween.AsyncWaitForCompletion();
        tween.Kill();
    }
}
