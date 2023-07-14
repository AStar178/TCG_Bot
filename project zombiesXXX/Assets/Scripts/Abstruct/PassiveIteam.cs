using UnityEngine;

public abstract class PassiveIteam : MonoBehaviour {
    public Sprite Icon;
    public string namex;
    public string dependencies;
    public int level = 0; 
    public float ScaleTheScaling = 1;
    public int DivedTheScaling = 1;
    public AnimationCurve ScalingLevel;
    public virtual void OnStart(PlayerState playerState)
    {

    }
    public virtual State OnUpdateAddOverTime(PlayerState playerState , ref State state)
    {
        return state;
    }
    public virtual State OnUpdateMultiyMYHEADISDIEING(PlayerState playerState , ref State state)
    {
        return state;
    }
    public virtual void OnUpdate(PlayerState playerState)
    {
        
    }
    public virtual void OnUseSkill(PlayerState playerState)
    {
        
    }

    public virtual void OnDrop(PlayerState playerState)
    {

    }
    public DamageData CreatDamage(float damage , PlayerState playerState)
    {
        DamageData damageData = new DamageData();
        damageData.DamageAmount = damage;
        damageData.target = playerState.Player.findTarget.transform;
        return damageData;
    }
    public float Scaling()
    {
        if (level == 0)
            return 0;
        float x = 0;
        for (int i = 0; i < level; i++)
        {
            x += (ScalingLevel.Evaluate(i/10));
        }
        return (x + (level * 0.1f) * ScaleTheScaling) / DivedTheScaling;
    }
    public bool useskill;
    public Sprite IconSkill;
    public string namexSkill;
    public string dependenciesSkill;
}