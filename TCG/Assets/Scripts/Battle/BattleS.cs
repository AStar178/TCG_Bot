using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;
using UnityEngine;
using System;

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
    [Space]
    public Action<float> AngerChangeEvent;
    public EventUserBattelS events;
    #endregion

    #region PlayerStat
    public int PlayerAtk;
    public int PlayerDef;
    public int Anger;
    [Range(0 , 100)]
    public int Ult_Point;

    public int EnemyAtk;
    public int EnemyDef;

    public int rng;
    private int Ult_Ponit_Max = 100;
    #endregion

    #region Misc

    AbilitySetter AS;
    public GameObject particl;
    public GameObject Popup;
    public GameObject dummy;
    public GameObject ExpEffect;

    #endregion

    private void Start()
    {
        AS = GetComponent<AbilitySetter>();
        BattleStart();
    }

    internal void DamageEnemy(int value)
    {
        EnemyDef -= value;
        Ult(Anger);
        DamPop(EnemyKnight.gameObject, value.ToString());
    }
    internal void DamageTarget(GameObject target , int value)
    {
        EnemyDef -= value;
        Ult(Anger);
        DamPop(target.gameObject, value.ToString());
    }
    internal void DamagePlayer(int value)
    {
        PlayerDef -= value;
        DamPop(PlayerKnight.gameObject, value.ToString());
    }
    internal void ExpEffectUse(Transform target , float shakeTime , float ShakeAmount)
    {
        var exp = Instantiate(ExpEffect , target.position , Quaternion.identity);


        var shake = new Vector2(shakeTime , ShakeAmount);
        events.shake.Rasise(shake);

        Destroy(exp , 3);
    }

    #region Ult

    private void Ult(int Anger_Point)
    {
        Ult_Point += (int)Anger;

        Ult_Point = Mathf.Min(Ult_Point , Ult_Ponit_Max);
        AngerChangeEvent?.Invoke(Ult_Point);
        if (Ult_Point == Ult_Ponit_Max) 
            ULTREADY();
    }

    private void ULTREADY()
    {
        ExpEffectUse(EnemyKnight.transform , 10 , 10);
    }

    #endregion

    void BattleStart()
    {
        PlayerKnight.sprite = Util.PlayerBag.PCards[0].shape;

        EnemyKnight.sprite = EnemyCard.shape;

        PlayerAtk = Util.PlayerBag.PCards[0].attack;
        PlayerDef = Util.PlayerBag.PCards[0].deff;
        Anger = Util.PlayerBag.PCards[0].Anger;

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

    public void DamPop(GameObject Targ, string dam, bool Heal = false)
    {
        Vector3 trans = new Vector3(Targ.transform.position.x, Targ.transform.position.y + 3, Targ.transform.position.z);
        GameObject j = Instantiate(Popup, trans, Quaternion.identity);
        j.GetComponent<TextMeshPro>().text = dam;
        if (Heal == false)
        { j.GetComponent<TextMeshPro>().color = Color.red; } else j.GetComponent<TextMeshPro>().color = Color.green;
        j.transform.DOMoveY(j.transform.position.y + 2, 1);
        j.transform.DOScaleX(0, 1);
        j.transform.DOScaleY(0, 1);
        Destroy(j, 1);

    }

    public GameObject Dummy(float x, float y, float z, Color color)
    {
        GameObject g = Instantiate(dummy);
        g.GetComponent<SpriteRenderer>().color = color;
        g.transform.position = new Vector3 (x, y, z);
        return g;
    }

    public void destroy(GameObject g)
    {
        Destroy(g);
    }

    public GameObject Particl(float x, float y, float z, float Time)
    {
        GameObject g = Instantiate(particl);
        g.transform.position = new Vector3(x, y, z);
        Destroy(g, Time);
        return g;
    }

    public void changeColor(Color color, SpriteRenderer knight)
    {
        knight.DOColor(color, 0);
    }

    #region UI
    public Ability ability;

    void SkillTurn()
    {

        for (int i = 0; i < Util.PlayerBag.PCards[0].skills.Count; i++)
        {
            
            Util.UI.Attack_Buttom[i].GetComponent<Image>().sprite = Util.PlayerBag.PCards[0].skills[i].icon;
            AS.SkillLister(Util.PlayerBag.PCards[0].skills[i], Util.UI.Attack_Buttom[i].GetComponent<Button>());

        }
       

    }

    #endregion

    void RNG(int a, int b)
    {
        rng = UnityEngine.Random.Range(a, b);
    }
    
}
