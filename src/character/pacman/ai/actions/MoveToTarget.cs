using Godot;
using System.Threading.Tasks;

public partial class MoveToTarget : BT_ActionNode
{
    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D actorNode2D)
            return Task.FromResult(NodeState.FAILURE);

        if (!blackboard.HasKey("TargetPosition"))
            return Task.FromResult(NodeState.FAILURE);

        var target = blackboard.GetValue("TargetPosition").As<Vector2>();
        var speed = blackboard.HasKey("CharacterSpeed") ? blackboard.GetValue("CharacterSpeed").As<float>() : 120f;
        var delta = (float)actor.GetPhysicsProcessDeltaTime();

        actorNode2D.GlobalPosition = actorNode2D.GlobalPosition.MoveToward(target, speed * delta);

        // Consider arrived when very close
        if (actorNode2D.GlobalPosition.DistanceTo(target) <= (blackboard.HasKey("DistanceThreshold") ? blackboard.GetValue("DistanceThreshold").As<float>() : 8f))
        {
            return Task.FromResult(NodeState.SUCCESS);
        }

        return Task.FromResult(NodeState.RUNNING);
    }
}
