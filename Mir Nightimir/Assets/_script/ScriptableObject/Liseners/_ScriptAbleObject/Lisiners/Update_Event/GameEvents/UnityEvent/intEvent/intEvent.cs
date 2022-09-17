using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "NewEvent/Event_int")]
public class intEvent : BaseGameEvent<int>
{
    public void Rasise() => Rasise(new int());
}

