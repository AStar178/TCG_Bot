using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quest : MonoBehaviour
{
    // Create a script called on death trigger so it trigger the quest when something dies

    // this is THE STAGE OF THE QUEST DONT GET CONFUSED WITH THE REAL CURRENT ACTIVE QUEST
    public int CurrentActive;
    public List<Quests> Quests;
    [HideInInspector]
    public Rigidbody QuestGiver;
    int update;

    public void UpdateQuest()
    {
        Quests[CurrentActive].ToFinishTheQuest--;
        if (Quests[CurrentActive].ToFinishTheQuest <= 0)
        {
            // Show Quest Completed
            Static.UiManager.PopUp(Quests[CurrentActive].FinishDialouge);
            // Add Exp and Gold if was the last

            if (Quests.Count > 1)
            {
                CurrentActive++;
                update = 1;
            }
        }
    }
    public void ShowQuestStartDialouge()
    {
        if (Static.UiManager.CanPopUp())
        {
            // Show Quest Start
            Static.UiManager.PopUp(Quests[CurrentActive].StartDialouge);
            update = 0;
        }
    }

    private void Update()
    {
        if (update == 1)
            ShowQuestStartDialouge();
    }
}

[System.Serializable]
public class Quests
{
    [TextArea]
    public string StartDialouge;
    public int ToFinishTheQuest;
    // this well set for the NPCLayer whiel the quest is active
    [TextArea]
    public string Dialouge;
    public int Exp, Gold;
    // check this if want to end the quest on talk
    public bool TalkToFinishQuest;
    [TextArea]
    public string FinishDialouge;
}