using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Card", menuName = "Cards/Knigh/Normal")]
public class KnightCardN : ScriptableObject
{
    #region Add here then add down there
    [SerializeField]
    private Sprite Shape;
    [SerializeField]
    string Name;
    [SerializeField]
    string Description;
    [SerializeField]
    public Type Type;
    [Space]
    [SerializeField]
    int Attack;
    [SerializeField]
    int Deff;
    [Space]
    [SerializeField]
    int Oar;
    [SerializeField]
    int Lvl;
    [SerializeField]
    int AngerPoint;
    [Space]
    [SerializeField]
    List<Skill> Skill;
    #endregion

    public Sprite shape => Shape;
    public string name => Name;
    public string description => Description;
    public Type type => Type;
    public int attack => Attack;
    public int deff => Deff;
    public int oar => Oar;
    public int lvl => Lvl;
    public int Anger => AngerPoint;
    public List<Skill> skills => Skill;
}
