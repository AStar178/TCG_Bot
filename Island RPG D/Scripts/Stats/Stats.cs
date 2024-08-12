using Godot;
using System;

[GlobalClass]
public partial class Stats : Resource
{
    [ExportSubgroup("Default Stats")]
    [Export] public int Atk;
    [Export] public int Def;
    [Export] public int Health;
    [Export] public int Resource;
    [ExportSubgroup("Stats")]
    [Export] public int PWR; // Physical DMG
    [Export] public int MGK; // Magical DMG
    [Export] public int STA; // Health, Def, Magical Def
    [Export] public int AGI; // Crit Rate, Crit Dmg
    [Export] public int SPD; // Turn Order

    public Stats()
    {

    }
    public Stats(Stats a)
    {
        Atk = a.Atk;
        Def = a.Def;
        Health = a.Health;
        Resource = a.Resource;
        PWR = a.PWR;
        MGK = a.MGK;
        STA = a.STA;
        AGI = a.AGI;
        SPD = a.SPD;
    }
    public Stats(Stats a, Stats b)
    {
        Atk = a.Atk + b.Atk;
        Def = a.Def + b.Def;
        Health = a.Health + b.Health;
        Resource = a.Resource + b.Resource;
        PWR = a.PWR + b.PWR;
        MGK = a.MGK + b.MGK;
        STA = a.STA + b.STA;
        AGI = a.AGI + b.AGI;
        SPD = a.SPD + b.SPD;
    }
    public Stats Scaling(Stats a, Stats b)
    {
        Atk = a.Atk + (int)((float)a.Atk * ((float)b.Atk/100));
        Def = a.Def + (int)((float)a.Def * ((float)b.Def/100));
        Health = a.Health + (int)((float)a.Health * ((float)b.Health/100));
        Resource = a.Resource;
        PWR = a.PWR + (int)((float)a.PWR * ((float)b.PWR/100));
        MGK = a.MGK + (int)((float)a.MGK * ((float)b.MGK/100));
        STA = a.STA + (int)((float)a.STA * ((float)b.STA/100));
        AGI = a.AGI + (int)((float)a.AGI * ((float)b.AGI/100));
        SPD = a.SPD + (int)((float)a.SPD * ((float)b.SPD/100));

        return this;
    }
}

///How to Calculate Speed Breakpoints in HSR.
///Speed Breakpoints and the action order of characters are calculated based on the Action Value.
///Action value is equal to 10000 divided by the Speed of the character. 
///For example, if Seele has 134 Speed, then her Action Value is 74.62.
