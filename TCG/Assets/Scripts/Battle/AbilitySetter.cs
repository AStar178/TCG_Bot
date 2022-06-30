using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class AbilitySetter : MonoBehaviour
{
    #region Needed but there is no need to tuch again
    BattleS battle;
    private void Awake()
    {
        battle = gameObject.GetComponent<BattleS>();
    }
    #endregion

    #region Abilities

    Slash SlashN = new Slash();
    Ora Ora = new Ora();
    TripleAttack TripleAttack = new TripleAttack();

    #endregion


    public void SkillLister(Skill s, Button skill)
    {
        if (s.animation.a == "SlashN")
        {
            battle.ability = SlashN;
            skill.onClick.AddListener(ASlashN);
        }
        if (s.animation.a == "Ora")
        {
            battle.ability = Ora;
            skill.onClick.AddListener(AOra);
        }
        if (s.animation.a == "TripleAttack")
        {
            battle.ability = Ora;
            skill.onClick.AddListener(ATripleAttack);
        }

    }

    public void ASlashN()
    {
        SlashN.PAttack(battle);
    }
    public void AOra()
    {
        Ora.PAttack(battle);
    }
    public void ATripleAttack()
    {
        TripleAttack.PAttack(battle);
    }
}
