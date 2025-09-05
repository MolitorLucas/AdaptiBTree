using Godot;
using System;

public partial class BT_AdaptiveNode : BT_Node
{

	public virtual void BeforeAdaptativeAction(Node actor, Blackboard blackboard = null)
	{
		GD.PrintErr("Before Adaptative  method not implemented in " + GetType().Name);
	}
	public virtual void AfterAdaptativeAction(Node actor, Blackboard blackboard = null)
	{
        GD.PrintErr("After Adaptative  method not implemented in " + GetType().Name);
	}
	
	public override NodeState Tick(Node actor, Blackboard blackboard = null)
	{
		if (GetChildren().Count == 0) return NodeState.FAILURE;
		if (GetChildren().Count > 1) return NodeState.FAILURE;
		if (GetChild(0) is BT_Node node)
		{
			if (!node.IsActive)
			{
				BeforeAdaptativeAction(actor, blackboard);
			}
			NodeState childState = node.Tick(actor, blackboard);
			if (childState != NodeState.RUNNING)
			{
				AfterAdaptativeAction(actor, blackboard);
			}
			return childState;
		}
		return NodeState.SUCCESS;
	}
	
}
