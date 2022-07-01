using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using DG.Tweening;

public abstract class MeleeAttack : Ability
{
    public virtual void PAttack(BattleS battle) => PlayerAttack(battle);

    async void PlayerAttack(BattleS battle)
    {
        Tween tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 3, Util.Setting.Speed * .25f);
        await tween.AsyncWaitForCompletion();
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 4, Util.Setting.Speed * 0.125f);
        battle.PlayerKnight.gameObject.transform.DORotate(new Vector3(0,0,-16), Util.Setting.Speed * .5f);
        await tween.AsyncWaitForCompletion();
        battle.DamageEnemy(battle.PlayerAtk);
        battle.changAttribiutText();
        battle.changeColor(Color.red, battle.EnemyKnight);
        battle.EnemyKnight.transform.DOShakePosition(.1f);
        await Task.Delay(10);
        battle.changeColor(Color.white, battle.EnemyKnight);
        battle.PlayerKnight.gameObject.transform.DORotate(new Vector3(0, 0, 0), Util.Setting.Speed * .25f);
        tween = battle.PlayerKnight.gameObject.transform.DOJump(new Vector3(battle.transform.position.x - 1, -3.5f, 0), 1,1,Util.Setting.Speed * .25f);
        await tween.AsyncWaitForCompletion();
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(-5.5f, Util.Setting.Speed * .5f);
        await tween.AsyncWaitForCompletion();
        tween.Kill();
    }

    public virtual void EAttack(BattleS battle) => EnemyAttack(battle);

    async void EnemyAttack(BattleS battle)
    {
        Tween tween = battle.EnemyKnight.gameObject.transform.DOMoveX(battle.transform.position.x - 4, Util.Setting.Speed);
        await tween.AsyncWaitForCompletion();
        battle.DamagePlayer(battle.EnemyAtk);
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
