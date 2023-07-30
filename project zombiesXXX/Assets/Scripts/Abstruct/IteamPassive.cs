using UnityEngine;

public abstract class IteamPassive : Iteam {
    public Sprite Icon;
    public string namex;
    public string dependencies;
    public int level = 0; 
    public float ScaleTheScaling = 1;
    public int DivedTheScaling = 1;
    public AnimationCurve ScalingLevel;
    public bool itemAdded = false;

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

}