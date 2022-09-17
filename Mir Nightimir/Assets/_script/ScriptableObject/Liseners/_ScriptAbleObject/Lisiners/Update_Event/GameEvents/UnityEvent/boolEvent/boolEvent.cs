using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "NewEvent/Event_bool")]
public class boolEvent : BaseGameEvent<bool>
{
    public void Rasise() => Rasise(new bool());
}

