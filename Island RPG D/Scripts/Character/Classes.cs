using Godot;
using System;
using System.Collections.Generic;

[GlobalClass]
public partial class Classes : Resource
{
    [Export] public string Name;
    [Export(PropertyHint.MultilineText)] public string Description;
    [Export] public Stats BaseStats;
    [Export] public int CurrentResource;
    [Export] public ArmorWight WearableArmor1;
    [Export] public ArmorWight WearableArmor2;
    [Export] public WeaponType UseableWeapons1;
    [Export] public WeaponType UseableWeapons2;
    Dictionary<string, Stats> Buffs = new Dictionary<string, Stats>();

    [Export] public Weapons Weapon;
    [Export] public Armors[] Armor;

    public Stats ResultStats()
    {
        Stats a = new Stats(BaseStats);

        foreach (Armors item in Armor)
            a = new Stats(a, item.Stats);

        if (Weapon != null)
            a = new Stats(a, Weapon.Stats);

        Dictionary<string, Stats>.ValueCollection values = Buffs.Values;
        foreach (Stats item in values)
            a = new Stats(a, item);

        return a;
    }
}
