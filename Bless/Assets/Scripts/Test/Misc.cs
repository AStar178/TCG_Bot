using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System.Threading.Tasks;

public class Misc : MonoBehaviour
{
    private bool smartDoor;
    private bool dooring;

    public void Toggle()
    {
        gameObject.SetActive(!gameObject.activeInHierarchy);
    }

    public async void Open(GameObject Door)
    {
        if (dooring)
            return;

        if (smartDoor == false)
        {
            dooring = true;
            Door.transform.DOMoveX(Door.transform.position.x - 2, 2);
            await Task.Delay(2000);
            dooring = false;
        }
        else
        {
            dooring = true;
            Door.transform.DOMoveX(Door.transform.position.x + 2, 2);
            await Task.Delay(2000);
            dooring = false;
        }

        smartDoor = !smartDoor;
    }
}
