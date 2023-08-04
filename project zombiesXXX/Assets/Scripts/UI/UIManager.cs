using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ImageBar HealthBar;
    [SerializeField]
    private ImageBar LeftBar;
    [SerializeField]
    List<Icons> Icons;

    #region UI Bars

    public void SetHealth(float current, float max, PlayerState playerState)
    {
        HealthBar.Set(current, max, Color.red);
        if (playerState.HasSecondBar() == true)
            return;

        SetLeftBar(current, max, Color.red);
    }

    public void SetLeftBar(float current, float max, Color Color)
    {
        LeftBar.Set(current, max, Color);
    }

    #endregion

    public void SetIcons(List<IteamSkill> iteamSkills)
    {
        foreach (var icon in Icons)
        {
            icon.SetIconImage(null);
        }

        for (int i = 0; i < iteamSkills.Count; i++)
        {
            if (i > 7)
                return;
            if (i < 3)
                Icons[i].SetIconImage(iteamSkills[i].IconSkill);
            else
                Icons[i + 2].SetIconImage(iteamSkills[i].IconSkill);
        }
    }

}
