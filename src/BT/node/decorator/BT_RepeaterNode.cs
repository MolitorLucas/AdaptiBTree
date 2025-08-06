using Godot;
using System;

[GlobalClass]
public partial class BT_RepeaterNode : BT_Node
{
    [Export]
    public int RepeatCount { get; set; } = 1;

    public BT_Node ChildNode { get; set; }

    public override void _Ready()
    {
        base._Ready();
        ChildNode = GetChild(0) as BT_Node;
    }

    public override NodeState Tick()
    {
        if (ChildNode == null)
        {
            GD.PrintErr("ChildNode is not set in BT_RepeaterNode.");
            return NodeState.FAILURE;
        }
        if(RepeatCount < 0)
        {
            GD.PrintErr("RepeatCount must be a non-negative integer.");
            return NodeState.FAILURE;
        }

        int count = 0;
        while (count < RepeatCount)
        {
            ChildNode.CurrentState = ChildNode.Tick();
            if (ChildNode.CurrentState == NodeState.SUCCESS)
                return NodeState.SUCCESS;
            count++;
            GD.Print($"RepeaterNode: Executed {count} times.");
        }
        if (ChildNode.CurrentState == NodeState.RUNNING)
            return NodeState.RUNNING;
        return NodeState.FAILURE;
    }
}
