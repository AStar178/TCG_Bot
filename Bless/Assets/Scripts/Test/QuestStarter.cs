using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestStarter : Intractable
{
    private bool isStarted;
    public Quest Quest;
    public bool createNPC;
    public int NPCNumber;
    public bool CreateMarker;
    public GameObject jojo;
    public Vector3 loc;
    public Sprite Sprite;
    [Tooltip("1 = OnDestroy / 2 = On Range")]
    public int UpdateType;
    public float range;

    public override void Intract()
    {
        if (isStarted)
            return;

        Static.QuestC.AddQuest(createNPC, NPCNumber, CreateMarker, jojo, loc, Sprite, this, UpdateType, Quest);
        Static.UiManager.PopUp(Quest.Quests[Quest.CurrentActive].StartDialouge);
        isStarted = true;
    }
}

[System.Serializable]
public class Bill
{
    public bool onDeath;
    public bool onDestroy;
    public float onRangeRange;
    public bool onRange;
    public bool onIntract;
}