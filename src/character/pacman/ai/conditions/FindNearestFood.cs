using Godot;
using System;

public partial class FindNearestFood : BT_ConditionNode
{
    [Export]
    public string FoodGroupName { get; set; } = "Food";

    protected override bool CheckCondition(Node actor, Blackboard blackboard)
    {
        var actorNode = actor as Node2D;
        if (actorNode == null) return false;

        var best = null as Node2D;
        float bestDist = float.MaxValue;

        var root = actor.GetTree().Root;
        var nodes = root.GetChildren();

        foreach (var n in nodes)
        {
            if (n is Node2D node2d && node2d.IsInGroup(FoodGroupName))
            {
                var d = actorNode.GlobalPosition.DistanceTo(node2d.GlobalPosition);
                if (d < bestDist)
                {
                    bestDist = d;
                    best = node2d;
                }
            }
        }

        if (best != null)
        {
            blackboard.SetValue("TargetPosition", best.GlobalPosition);
            return true;
        }

        return false;
    }
}
