using Godot;
using System;

public partial class IsPacmanNearby : BT_ConditionNode
{
    protected override bool CheckCondition(Node actor, Blackboard blackboard)
    {
        if (!blackboard.HasKey("Pacman")) return false;
        if (!blackboard.HasKey("DetectionDistance")) return false;

        var pacman = blackboard.GetValue("Pacman").As<Node2D>();
        if (pacman == null) return false;

        var actorNode = actor as Node2D;
        if (actorNode == null) return false;

        var dist = actorNode.GlobalPosition.DistanceTo(pacman.GlobalPosition);
        return dist <= blackboard.GetValue("DetectionDistance").As<float>();
    }
}
