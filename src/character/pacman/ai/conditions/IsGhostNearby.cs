using Godot;
using System;

public partial class IsGhostNearby : BT_ConditionNode
{
    protected override bool CheckCondition(Node actor, Blackboard blackboard)
    {
        if (!blackboard.HasKey("Ghosts")) return false;
        if (!blackboard.HasKey("FearDistance")) return false;

        var ghosts = blackboard.GetValue("Ghosts").As<Godot.Collections.Array>();
        var fear = blackboard.GetValue("FearDistance").As<float>();

        var actorNode = actor as Node2D;
        if (actorNode == null) return false;
        var actorPos = actorNode.GlobalPosition;
        for (int i = 0; i < ghosts.Count; i++)
        {
            var v = ghosts[i].As<Node>();
            if (v != null)
            {
                var g = v as Node2D;
                if (g != null)
                {
                    if (actorPos.DistanceTo(g.GlobalPosition) <= fear)
                        return true;
                }
            }
        }
        return false;
    }
}
