using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "NewEvent/Event_float")]
public class floatEvent : BaseGameEvent<float>
{
    public void Rasise() => Rasise(new float());
}

