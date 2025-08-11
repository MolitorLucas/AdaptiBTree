using Godot;
using System.Threading.Tasks;
using System;

public partial class GoToMousePosition : BT_ActionNode
{
	public override async Task<NodeState> Execute(Node2D actor, Blackboard blackboard)
	{

		var mousePosition = actor.GetGlobalMousePosition();

		if (actor.GlobalPosition.DistanceTo(mousePosition) < 100)
		{
			return NodeState.SUCCESS;
		}
		actor.GlobalPosition = actor.GlobalPosition.MoveToward(mousePosition, blackboard.GetValue("character_speed").As<float>() * (float)actor.GetPhysicsProcessDeltaTime());

		return NodeState.RUNNING;
	}
}
