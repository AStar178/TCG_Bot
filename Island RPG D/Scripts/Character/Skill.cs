using Godot;
using System;

[GlobalClass]
public partial class Skill : Resource
{
    [Export] public string Name;
    [Export(PropertyHint.MultilineText)] public string Description;
    [Export] public Target Target;
    [Export] public int Base;
    [Export] public bool AddWeapon;
    [Export] public bool AddSTR;
    [Export] public bool AddWis;
}
