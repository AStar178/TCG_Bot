using System;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "Iteam", menuName = "project zombiesXXX/StateScriptAbleObject", order = 0)]
public class StateScriptAbleObject : ScriptableObject {
    
    public Sprite icone;
    public string namex;
    public string discrapsen;
    public State state;
    public bool ModeMulity;
    public IteamPassive passiveIteam;
    public IteamSkill SkillIteam;
    public bool IteamAddAready;
    public RpgHelper.IteamType iteamTypo;
    string CreatedGameObject;



    public void CreatNewUnityPrefabs()
    {
        GameObject gameObject = new();
        gameObject.AddComponent<BoxCollider>().size = Vector3.one * 0.01f;
        var x = gameObject.AddComponent<IteamforChest>();
        x.stateScriptAbleObject = this;
        x.iteamTypo = iteamTypo;
        CreatedGameObject =  "Assets/Scripts/PassiveIteams/prefabs/"+namex+".prefab";
        PrefabUtility.SaveAsPrefabAsset(gameObject , "Assets/Scripts/PassiveIteams/prefabs/"+namex+".prefab");
        DestroyImmediate(gameObject);
        
    }
    public void Applay()
    {
        var wo = AssetDatabase.LoadAssetAtPath<GameObject>(CreatedGameObject);
        if (wo == null)
        {
            string msg = $"Assets/Scripts/PassiveIteams/prefabs/{namex}.prefab";
            wo = AssetDatabase.LoadAssetAtPath<GameObject>(msg);
        }
        wo.GetComponent<IteamforChest>().stateScriptAbleObject = this;
        wo.GetComponent<IteamforChest>().iteamTypo = iteamTypo;
       if (wo.TryGetComponent<IteamPassive>(out var s))
       {
            passiveIteam = s;
            s.namex = namex;
       }
        if (wo.TryGetComponent<IteamSkill>(out var sx))
       {
            SkillIteam = sx;
            sx.namexSkill = namex;
        }

    }
    public void AdditToPlayer()
    {
        var s = FindFirstObjectByType<PlayerState>();
        if (ModeMulity == true)
        {
            s.IteamsMulty.Add(this);
            return;
        }
        s.IteamsAdd.Add(this);
    }
    public GameObject GiveIteam()
    {
        if (SkillIteam != null)
            return SkillIteam.gameObject;
        if (passiveIteam != null)
            return passiveIteam.gameObject;
        return null;
    }
}
