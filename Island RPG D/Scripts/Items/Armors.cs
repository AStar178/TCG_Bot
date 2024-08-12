using Godot;
using System;

[GlobalClass]
public partial class Armors : Resource
{
    [Export] public string Name;
    [Export(PropertyHint.MultilineText)] public string Description;
    [Export] public ArmorWight Type;
    [Export] public EqupmentSlot Slot;
    [Export] public Stats Stats;
}
