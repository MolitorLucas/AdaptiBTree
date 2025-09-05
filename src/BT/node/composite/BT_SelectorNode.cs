using Godot;
using System;
using System.Threading.Tasks;

[GlobalClass]
public partial class BT_SelectorNode : BT_Node
{
    public override NodeState Tick(Node actor, Blackboard blackboard)
    {
        foreach (var child in GetChildren())
        {
            if (child is BT_Node node)
            {
                node.CurrentState = node.Tick(actor, blackboard);
                if (node.CurrentState == NodeState.SUCCESS)
                    return NodeState.SUCCESS;
                if (node.CurrentState == NodeState.RUNNING)
                    return NodeState.RUNNING;
            }
        }

        return NodeState.FAILURE;
    }
}
