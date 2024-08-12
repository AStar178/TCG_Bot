using Godot;
using System;

[GlobalClass]
public partial class Weapons : Resource
{
    [Export] public string Name;
    [Export(PropertyHint.MultilineText)] public string Description;
    [Export] public WeaponType Type;
    [Export] public Stats Stats;
}
