using Godot;
using System;

[GlobalClass]
public partial class BT_SequenceNode : BT_Node
{
    public override NodeState Tick(Node actor, Blackboard blackboard)
    {
        foreach (var child in GetChildren())
        {
            if (child is BT_Node node)
            {
                node.CurrentState = node.Tick(actor, blackboard);
                if (node.CurrentState == NodeState.FAILURE)
                    return NodeState.FAILURE;
                if (node.CurrentState == NodeState.RUNNING)
                    return NodeState.RUNNING;
            }
        }

        return NodeState.SUCCESS;
    }
}
