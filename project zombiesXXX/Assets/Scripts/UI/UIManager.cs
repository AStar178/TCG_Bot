using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private ImageBar HealthBar;
    [SerializeField]
    private ImageBar LeftBar;

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
}
