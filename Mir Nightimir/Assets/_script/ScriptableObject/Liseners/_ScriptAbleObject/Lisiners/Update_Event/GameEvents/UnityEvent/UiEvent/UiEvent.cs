using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Event", menuName = "ScriptibaleObject/Event_String")]
public class UiEvent : BaseGameEvent<UiEventData>
{
    public void Rasise() => Rasise(new UiEventData());
}

