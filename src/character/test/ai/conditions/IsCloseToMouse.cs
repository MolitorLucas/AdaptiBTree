using Godot;
using System;

public partial class IsCloseToMouse : BT_ConditionNode
{

    protected override bool CheckCondition(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D node2d) return false;
        var mousePosition = node2d.GetGlobalMousePosition();
        var distance = node2d.GlobalPosition.DistanceTo(mousePosition);
        return distance <= blackboard.GetValue("DistanceThreshold").As<float>();
    }
}