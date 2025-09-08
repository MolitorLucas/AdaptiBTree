using Godot;
using System.Threading.Tasks;
using System.Linq;

public partial class SeekPickup : BT_ActionNode
{
    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D actor2d) return Task.FromResult(NodeState.FAILURE);

        var pickups = actor.GetTree().GetNodesInGroup("pickups").OfType<Area2D>();
        if (!pickups.Any()) return Task.FromResult(NodeState.FAILURE);

        Area2D nearest = null;
        float best = float.MaxValue;
        foreach (var p in pickups)
        {
            var dist = p.GlobalPosition.DistanceTo(actor2d.GlobalPosition);
            if (dist < best)
            {
                best = dist;
                nearest = p;
            }
        }

        if (nearest == null) return Task.FromResult(NodeState.FAILURE);

        var speed = blackboard["CharacterSpeed"].As<float>();
        var delta = (float)actor2d.GetPhysicsProcessDeltaTime();
        actor2d.GlobalPosition = actor2d.GlobalPosition.MoveToward(nearest.GlobalPosition, speed * delta);

        return Task.FromResult(NodeState.SUCCESS);
    }
}
