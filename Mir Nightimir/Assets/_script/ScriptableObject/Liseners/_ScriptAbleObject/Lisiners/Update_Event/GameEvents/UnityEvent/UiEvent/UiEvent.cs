using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "NewEvent/Event_Ui")]
public class UiEvent : BaseGameEvent<UiEventData>
{
    public void Rasise() => Rasise(new UiEventData());
}

