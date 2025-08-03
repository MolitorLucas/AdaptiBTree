using Godot;
using System;

[GlobalClass]
public partial class BT_InverterNode : BT_Node
{
    [Export]
    public BT_Node ChildNode { get; set; }

    public override NodeState Tick()
    {
        if (ChildNode == null)
            return NodeState.FAILURE;

        var childState = ChildNode.Tick();
        if (childState == NodeState.FAILURE)
            return NodeState.SUCCESS;
        if (childState == NodeState.SUCCESS)
            return NodeState.FAILURE;
        return NodeState.RUNNING;
    }
}
