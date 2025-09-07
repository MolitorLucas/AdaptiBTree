using Godot;
using System.Threading.Tasks;
using System;

public partial class GoToMousePosition : BT_ActionNode
{
	public override async Task<NodeState> Execute(Node actor, Blackboard blackboard)
	{
        if (actor is not Node2D actorNode2D) return NodeState.FAILURE;
		GD.Print("Actor name: " + actorNode2D.Name + " Type: " + actorNode2D.GetType().Name);
        var mousePos = actorNode2D.GetGlobalMousePosition();
		actorNode2D.GlobalPosition = actorNode2D.GlobalPosition.MoveToward(mousePos, blackboard["CharacterSpeed"].As<float>() * (float)actor.GetPhysicsProcessDeltaTime());
	
		return NodeState.SUCCESS;
	}
}
