using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Animation", menuName = "Cards/Skill/Animation")]
public class AbilityObject : ScriptableObject
{
    [SerializeField]
    string A;

    public string a => A;
}
