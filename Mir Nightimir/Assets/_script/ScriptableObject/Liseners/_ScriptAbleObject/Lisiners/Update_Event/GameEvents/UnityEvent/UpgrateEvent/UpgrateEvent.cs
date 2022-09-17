using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "NewEvent/Event_Upgrate")]
public class UpgrateEvent : BaseGameEvent<UpgrateEventData>
{
    public void Rasise() => Rasise(new UpgrateEventData());
}

