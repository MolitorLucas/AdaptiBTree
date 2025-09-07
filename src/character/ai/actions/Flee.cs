using Godot;
using System;
using System.Threading.Tasks;

public partial class Flee : BT_ActionNode
{
    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D actor2d)
            return Task.FromResult(NodeState.FAILURE);

        var target = blackboard["SafePosition"].As<Vector2>();
        var speed = blackboard["CharacterSpeed"].As<float>();
        var delta = (float)actor2d.GetPhysicsProcessDeltaTime();
        actor2d.GlobalPosition = actor2d.GlobalPosition.MoveToward(target, speed * delta);

        return Task.FromResult(NodeState.SUCCESS);
    }
}