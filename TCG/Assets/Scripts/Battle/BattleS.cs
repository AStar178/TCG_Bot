using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine;

public class BattleS : MonoBehaviour
{
    #region
    public KnightCardN EnemyCard;
    public SpriteRenderer PlayerKnight;
    public SpriteRenderer EnemyKnight;
    [Space]
    public TextMeshPro PDam;
    public TextMeshPro PDef;
    public TextMeshPro EDam;
    public TextMeshPro EDef;
    #endregion

    #region PlayerStat
    public int PlayerAtk;
    public int PlayerDef;

    public int EnemyAtk;
    public int EnemyDef;

    public int rng;
    #endregion

    #region Misc

    AbilitySetter AS;

    #endregion

    private void Start()
    {
        AS = GetComponent<AbilitySetter>();
        BattleStart();
    }

    void BattleStart()
    {
        PlayerKnight.sprite = Util.PlayerBag.PCards[0].shape;

        EnemyKnight.sprite = EnemyCard.shape;

        PlayerAtk = Util.PlayerBag.PCards[0].attack;
        PlayerDef = Util.PlayerBag.PCards[0].deff;

        EnemyAtk = EnemyCard.attack;
        EnemyDef = EnemyCard.deff;

        SkillTurn();
        changAttribiutText();

    }

    public void changAttribiutText()
    {
        PDam.text = PlayerAtk.ToString();
        PDef.text = PlayerDef.ToString();
        EDam.text = EnemyAtk.ToString();
        EDef.text = EnemyDef.ToString();
    }

    public void changeColor(Color color, SpriteRenderer knight)
    {
        knight.DOColor(color, 0);
    }

    #region UI
    public Ability ability;

    void SkillTurn()
    {
        Util.UI.AttackButton.GetComponent<Image>().sprite = Util.PlayerBag.PCards[0].skills[0].icon;
        Util.UI.Skill1Button.GetComponent<Image>().sprite = Util.PlayerBag.PCards[0].skills[1].icon;
        Util.UI.Skill2Button.GetComponent<Image>().sprite = Util.PlayerBag.PCards[0].skills[2].icon;
        Util.UI.Skill3Button.GetComponent<Image>().sprite = Util.PlayerBag.PCards[0].skills[3].icon;

        AS.SkillLister(Util.PlayerBag.PCards[0].skills[0], Util.UI.AttackButton.GetComponent<Button>());
        AS.SkillLister(Util.PlayerBag.PCards[0].skills[1], Util.UI.Skill1Button.GetComponent<Button>());
        AS.SkillLister(Util.PlayerBag.PCards[0].skills[2], Util.UI.Skill2Button.GetComponent<Button>());
        AS.SkillLister(Util.PlayerBag.PCards[0].skills[3], Util.UI.Skill3Button.GetComponent<Button>());

    }

    #endregion

    #region Skills Skillz


    public void SlashN()
    {
        //testo.PAttack(this);
    }

    #endregion

    void RNG(int a, int b)
    {
        rng = Random.Range(a, b);
    }
}
