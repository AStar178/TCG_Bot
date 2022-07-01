using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Ora : Ability
{
    public override void PAttack(BattleS battle)
    {
        PlayerAttack(battle);
    }


    async void PlayerAttack(BattleS battle)
    {
        float ma = (float)3.8/(float)battle.PlayerAtk;
        float Value_Atk = 1f - ma;
        float atk = (Value_Atk) * 100;
        int atkint = (int)atk;
        

        Tween tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 3, Util.Setting.Speed * .25f);
        await tween.AsyncWaitForCompletion();
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 4, Util.Setting.Speed * 0.0125f);
        battle.PlayerKnight.gameObject.transform.DORotate(new Vector3(0, 0, -16), Util.Setting.Speed * 0.0125f);
        await tween.AsyncWaitForCompletion();
        battle.DamageEnemy(atkint);
        battle.changAttribiutText();
        battle.changeColor(Color.red, battle.EnemyKnight);
        battle.EnemyKnight.transform.DOShakePosition(.1f);
        await Task.Delay(10);
        battle.changeColor(Color.white, battle.EnemyKnight);
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 3, Util.Setting.Speed * 0.01125f);
        await tween.AsyncWaitForCompletion();
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 4, Util.Setting.Speed * 0.0125f);
        await tween.AsyncWaitForCompletion();
        battle.DamageEnemy(atkint);
        battle.changAttribiutText();
        battle.changeColor(Color.red, battle.EnemyKnight);
        battle.EnemyKnight.transform.DOShakePosition(.1f);
        await Task.Delay(10);
        battle.changeColor(Color.white, battle.EnemyKnight);
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 3, Util.Setting.Speed * 0.0125f);
        await tween.AsyncWaitForCompletion();
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 4, Util.Setting.Speed * 0.0125f);
        await tween.AsyncWaitForCompletion();
        battle.DamageEnemy(atkint);
        battle.changAttribiutText();
        battle.changeColor(Color.red, battle.EnemyKnight);
        battle.EnemyKnight.transform.DOShakePosition(.1f);
        await Task.Delay(10);
        battle.changeColor(Color.white, battle.EnemyKnight);
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 3, Util.Setting.Speed * 0.0125f);
        await tween.AsyncWaitForCompletion();
        tween = battle.PlayerKnight.gameObject.transform.DOMoveX(battle.transform.position.x + 4.4f, Util.Setting.Speed * 0.0125f);
        await tween.AsyncWaitForCompletion();
        battle.DamageEnemy(atkint + 3);
        battle.changAttribiutText();
        battle.changeColor(Color.red, battle.EnemyKnight);
        battle.EnemyKnight.transform.DOShakePosition(.1f);
        await Task.Delay(10);
        battle.changeColor(Color.white, battle.EnemyKnight);
        battle.PlayerKnight.gameObject.transform.DOJump(new Vector3(-5.5f, -3.5f, 0), 1f, 1, Util.Setting.Speed * 0.25f);
        tween = battle.EnemyKnight.gameObject.transform.DOJump(new Vector3(5.5f, -3.5f, 0), 0.5f, 1, Util.Setting.Speed * 0.125f);
        battle.PlayerKnight.gameObject.transform.DORotate(new Vector3(0, 0, 0), Util.Setting.Speed * .25f);
        await tween.AsyncWaitForCompletion();
        tween.Kill();
    }
}
