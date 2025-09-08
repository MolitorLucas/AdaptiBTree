using Godot;
using System;

public partial class IsCloseToOpponent : BT_ConditionNode
{
    [Export]
    public float Threshold { get; set; } = 100f;

    protected override bool CheckCondition(Node actor, Blackboard blackboard)
    {
        if (actor is not Agent agent) return false;
        var opponent = agent.Opponent;
        if (opponent == null) return false;
        if (actor is not Node2D actor2d) return false;
        return actor2d.GlobalPosition.DistanceTo(opponent.GlobalPosition) <= Threshold;
    }
}
