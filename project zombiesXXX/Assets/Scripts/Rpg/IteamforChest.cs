using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IteamforChest : Interactable
{
    public StateScriptAbleObject stateScriptAbleObject;
    public RpgHelper.IteamType iteamTypo;

    public void Start()
    {
        caninteracted = false;
    }

    public void fixit()
    {
        caninteracted = true;
    }

    public override string GetText()
    {
        string a = stateScriptAbleObject.namex;

        return a;
    }
}
