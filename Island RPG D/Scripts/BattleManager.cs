using Godot;
using System;

///<summary> How to Calculate Speed Breakpoints in HSR. </summary>
///Speed Breakpoints and the action order of characters are calculated based on the Action Value.
///Action value is equal to 10000 divided by the Speed of the character. 
///For example, if Seele has 134 Speed, then her Action Value is 74.62.


public partial class BattleManager : Node
{
    [Export] public Classes Player;

    public override void _Ready()
    {
        GD.Print(10000 / Player.ResultStats().SPD);
    }
}
