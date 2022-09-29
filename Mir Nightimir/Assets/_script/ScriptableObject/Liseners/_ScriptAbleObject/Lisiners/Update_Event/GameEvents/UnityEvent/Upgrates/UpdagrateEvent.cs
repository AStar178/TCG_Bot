using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "NewEvent/Event_Ui")]
public class UpdagrateEvent : BaseGameEvent<UpdagrateEventdata>
{
    public void Rasise() => Rasise(new UpdagrateEventdata());
}

