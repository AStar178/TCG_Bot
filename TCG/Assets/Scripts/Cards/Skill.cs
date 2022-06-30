using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Cards/Skill/Ability")]
public class Skill : ScriptableObject
{
    [SerializeField]
    Sprite Icon;
    [SerializeField]
    string Name;
    [SerializeField]
    string Describ;
    [Space]
    [SerializeField]
    int PlusDam;
    [SerializeField]
    int PlusDef;
    [Space]
    [SerializeField]
    bool Self;
    [SerializeField]
    bool Heal;
    [SerializeField]
    AbilityObject Animation;


    public Sprite icon => Icon;
    public string name => Name;
    public string describ => Describ;
    public int plusDam => PlusDam;
    public int plusDef => PlusDef;
    public bool self => Self;
    public bool heal => Heal;
    public AbilityObject animation => Animation;
}
