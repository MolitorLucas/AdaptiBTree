using Godot;
using System.Threading.Tasks;

public partial class ComputeFleeTarget : BT_ActionNode
{
    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        var actorNode = actor as Node2D;
        if (actorNode == null) return Task.FromResult(NodeState.FAILURE);

        if (!blackboard.HasKey("Ghosts") || !blackboard.HasKey("FearDistance"))
            return Task.FromResult(NodeState.FAILURE);

        var ghosts = blackboard.GetValue("Ghosts").As<Godot.Collections.Array>();
        var fear = blackboard.GetValue("FearDistance").As<float>();

        Node2D nearest = null;
        float bestDist = float.MaxValue;
        for (int i = 0; i < ghosts.Count; i++)
        {
            var v = ghosts[i].As<Node>();
            if (v == null) continue;
            if (v is not Node2D ghost) continue;
            var d = actorNode.GlobalPosition.DistanceTo(ghost.GlobalPosition);
            if (d < bestDist)
            {
                bestDist = d;
                nearest = ghost;
            }
        }

        if (nearest == null) return Task.FromResult(NodeState.FAILURE);

        // Compute flee direction opposite to the ghost
        var dir = (actorNode.GlobalPosition - nearest.GlobalPosition).Normalized();
        var fleeDistance = fear * 1.5f;
        var target = actorNode.GlobalPosition + dir * fleeDistance;
        blackboard.SetValue("TargetPosition", target);
        return Task.FromResult(NodeState.SUCCESS);
    }
}
