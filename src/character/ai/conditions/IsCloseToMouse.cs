using Godot;
using System;

public partial class IsCloseToMouse : BT_ConditionNode
{

    protected override bool CheckCondition(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D actor2d)
            return false;

        var mousePosition = actor2d.GetGlobalMousePosition();
        var distance = actor2d.GlobalPosition.DistanceTo(mousePosition);
        return distance <= blackboard["DistanceThreshold"].As<float>();
    }
}