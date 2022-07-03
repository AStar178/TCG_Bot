using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public class FireBall : Ability
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

        Tween tween = Attacker.DORotate(new Vector3(0, 0, -16), Util.Speed * .25f);
        await tween.AsyncWaitForCompletion();
        Attacker.DORotate(new Vector3(0, 0, 0), Util.Speed * .125f);
        tween = Attacker.DOMoveX(Attacker.position.x - .5f, Util.Speed * .125f);
        Attacker.DOMoveY(Attacker.position.y - Random.Range(-.5f, .6f), Util.Speed * .125f);
        await tween.AsyncWaitForCompletion();
        GameObject s = battle.Dummy(-4.5f, Attacker.position.y, Attacker.position.z, Color.red);
        tween = Attacker.DOMoveX(Attacker.position.x + .5f, Util.Speed * .25f);
        Attacker.DOMoveY(-3.3f, Util.Speed * .25f);
        s.transform.DOMoveX(target.position.x, Util.Speed * .125f);
        tween = s.transform.DOMoveY(target.position.y, Util.Speed * .125f);
        s.transform.DOScaleY(.3f, Util.Speed * .125f);
        s.transform.DOScaleX(1.2f, Util.Speed * .125f);
        await tween.AsyncWaitForCompletion();
        GameObject p = battle.Particl(s.transform.position.x, s.transform.position.y, s.transform.position.z, .5f);
        battle.destroy(s);
        TokeDamage(battle, atk, target);
        tween.Kill();
    }

    async void TokeDamage(BattleS battle, int atk, Transform target)
    {
        battle.DamageTarget(target.gameObject , atk);
        battle.changAttribiutText();
        battle.changeColor(Color.red, battle.EnemyKnight);
        Tween tween = target.DOMoveX(target.position.x + 1, Util.Speed * .125f);
        target.DOMoveY(target.position.y - Random.Range(-0.5f, .6f), Util.Speed * .125f);
        await tween.AsyncWaitForCompletion();
        target.DOMoveX(5.5f, Util.Speed * .125f);
        target.DOMoveY(-3.3f, Util.Speed * .125f);
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
