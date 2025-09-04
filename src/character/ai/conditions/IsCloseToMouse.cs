using Godot;
using System;

public partial class IsCloseToMouse : BT_ConditionNode
{

    protected override bool CheckCondition(Node2D actor, Blackboard blackboard)
    {
        var mousePosition = actor.GetGlobalMousePosition();
        var distance = actor.GlobalPosition.DistanceTo(mousePosition);
        return distance <= blackboard.GetValue("DistanceThreshold").As<float>();
    }
}