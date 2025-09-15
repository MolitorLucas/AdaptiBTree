using Godot;
using System.Threading.Tasks;
using System.Linq;

public partial class SeekPickup : BT_ActionNode
{
    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D actor2d) return Task.FromResult(NodeState.FAILURE);

        var nearest = blackboard["BestPickupFor"+actor2d.Name].As<Area2D>();
        if (nearest == null) return Task.FromResult(NodeState.FAILURE);
        var speed = blackboard["CharacterSpeed"].As<float>();
        var delta = (float)actor2d.GetPhysicsProcessDeltaTime();
        actor2d.GlobalPosition = actor2d.GlobalPosition.MoveToward(nearest.GlobalPosition, speed * delta);

        return Task.FromResult(NodeState.SUCCESS);
    }
}
