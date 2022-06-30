using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Type", menuName = "Cards/Type")]
public class Type : ScriptableObject
{
    [SerializeField]
    string Name;
    [SerializeField]
    string Des;

    public string name => Name;
    public string des => Name;
}
