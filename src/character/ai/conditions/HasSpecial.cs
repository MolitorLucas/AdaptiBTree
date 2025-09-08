using Godot;
using System;

public partial class HasSpecial : BT_ConditionNode
{
    protected override bool CheckCondition(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D actor2d) return false;
        var stats = actor2d.GetNodeOrNull<CharacterStats>("CharacterStats");
        return stats != null && stats.HasSpecialPower;
    }
}
