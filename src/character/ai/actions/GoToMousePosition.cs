using Godot;
using System.Threading.Tasks;
using System;

public partial class GoToMousePosition : BT_ActionNode
{
	public override async Task<NodeState> Execute(Node2D actor, Blackboard blackboard)
	{
		actor.GlobalPosition = actor.GlobalPosition.MoveToward(actor.GetGlobalMousePosition(), blackboard.GetValue("CharacterSpeed").As<float>() * (float)actor.GetPhysicsProcessDeltaTime());

		return NodeState.SUCCESS;
	}
}
