using Godot;
using System;

[GlobalClass]
public partial class BT_RepeaterNode : BT_Node
{
    [Export]
    public int RepeatCount { get; set; } = -1;

    [Export]
    public BT_Node ChildNode { get; set; }

    public override NodeState Tick()
    {
        if (ChildNode == null)
            return NodeState.FAILURE;

        int count = 0;
        while (RepeatCount == -1 || count < RepeatCount)
        {
            var status = ChildNode.Tick();
            if (status == NodeState.RUNNING)
                return NodeState.RUNNING;
            if (status == NodeState.FAILURE)
                return NodeState.FAILURE;
            count++;
        }
        return NodeState.SUCCESS;
    }
}
