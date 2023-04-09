using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestC : MonoBehaviour
{
    List<GameObject> marked;

    GameObject created;
    public void AddQuest(bool CreateNPC, int NPCNumber, bool GiveThemMarker, GameObject Npc, Vector3 CreatingLoc, Sprite QuestSprite, QuestStarter QS = null, int updater = 0, Quest updater1 = null)
    {
        if (CreateNPC)
        {
            for (int i = 0; i < NPCNumber; i++)
            {
                if (updater == 0)
                    O(CreatingLoc, Npc, GiveThemMarker, QuestSprite);
                if (updater != 0)
                {
                    QuestUpdater a = O(CreatingLoc, Npc, GiveThemMarker, QuestSprite).AddComponent<QuestUpdater>();

                    if (updater == 1)
                    {
                        a.onDestroy = true;
                        a.UpdateThis = updater1;
                    }
                    if (updater == 2)
                    {
                        a.onRange = true;
                        a.onRangeRange = QS.range;
                        a.UpdateThis = updater1;
                    }
                }
            }
        }
    }

    private GameObject O(Vector3 loc, GameObject Npc, bool addMarker, Sprite QuestSprite)
    {
        created = null;
        created = Instantiate(Npc);
        created.transform.position = loc;
        if (addMarker)
        {
            created.AddComponent<Marker>().icon = QuestSprite;
            created.GetComponent<Marker>().dontStart = true;
            Static.Compass.AddMarker(created.GetComponent<Marker>());
        }

        return created;
    }
}
