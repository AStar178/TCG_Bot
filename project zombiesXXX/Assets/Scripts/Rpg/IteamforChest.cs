using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IteamforChest : Interactable
{
    public StateScriptAbleObject stateScriptAbleObject;
    public RpgHelper.IteamType iteamTypo;
    public Chest chest;

    public async void Start()
    {
        caninteracted = false;
        await System.Threading.Tasks.Task.Delay(100);

        if (chest == null)
            Destroy(this);
    }

    public void fixit()
    {
        caninteracted = true;
    }
    public override void OnInteracted()
    {
        Player.Current.PlayerState.AddIteam(stateScriptAbleObject);
        
        chest.Des();
    }
    public override string GetText()
    {
        string a = stateScriptAbleObject.namex;

        return a;
    }
}
