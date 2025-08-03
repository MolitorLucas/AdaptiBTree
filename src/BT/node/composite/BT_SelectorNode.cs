using Godot;
using System;

public partial class BT_SelectorNode : BT_Node
{
    public override NodeState Tick()
    {
        foreach (var child in GetChildren())
        {
            if (child is BT_Node node)
            {
                var result = node.Tick();
                if (result == NodeState.SUCCESS)
                    return NodeState.SUCCESS;
                if (result == NodeState.RUNNING)
                    return NodeState.RUNNING;
            }
        }

        return NodeState.FAILURE;
    }
}
