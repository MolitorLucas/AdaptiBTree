using Godot;
using System.Threading.Tasks;
using System;

public partial class GoToMousePosition : BT_ActionNode
{
	public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
	{
        if (actor is not Node2D actor2d)
            return Task.FromResult(NodeState.FAILURE);

        var target = actor2d.GetGlobalMousePosition();
		var speed = blackboard["CharacterSpeed"].As<float>();
		var delta = (float)actor2d.GetPhysicsProcessDeltaTime();
		actor2d.GlobalPosition = actor2d.GlobalPosition.MoveToward(target, speed * delta);

		return Task.FromResult(NodeState.SUCCESS);
	}
}
