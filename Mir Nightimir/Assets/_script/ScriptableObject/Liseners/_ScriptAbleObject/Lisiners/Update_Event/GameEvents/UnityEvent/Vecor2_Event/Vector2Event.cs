using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "NewEvent/Event_Vector2")]
public class Vector2Event : BaseGameEvent<Vector2>
{
    public void Rasise() => Rasise(new Vector2());
}

