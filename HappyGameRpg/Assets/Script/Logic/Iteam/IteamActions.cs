using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "IteamActions", menuName = "HappyGameRpg/Rpg/IteamActions", order = 0)]
public abstract class IteamActions : ScriptableObject 
{
    

    public abstract void OnUpdate();
    public abstract void OnStart();
    public abstract void OnEndGame();


}

[Serializable]
public struct ActionStruct
{

    public Action action;
    public IteamActions iteamActions;

}

public enum Action
{

    OnAutoAttack,
    OnAutoAttackPunch,
    OnSkillUsed,
    OnKillEnemy,
    OnHitEnemy,
    OnUpdate,
    OnStart,

}
