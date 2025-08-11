using Godot;
using System;

[GlobalClass]
public partial class BT_InverterNode : BT_Node
{
    [Export]
    public BT_Node ChildNode { get; set; }

    public override NodeState Tick(Node2D actor, Blackboard blackboard)
    {
        if (ChildNode == null)
            return NodeState.FAILURE;

        ChildNode.CurrentState = ChildNode.Tick(actor, blackboard);
        if (ChildNode.CurrentState == NodeState.FAILURE)
            return NodeState.SUCCESS;
        if (ChildNode.CurrentState == NodeState.SUCCESS)
            return NodeState.FAILURE;
        return NodeState.RUNNING;
    }
}
