using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptibaleObject/Event_String")]
public class UpgrateEvent : BaseGameEvent<UpgrateEventData>
{
    public void Rasise() => Rasise(new UpgrateEventData());
}

