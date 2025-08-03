using Godot;
using System;

[GlobalClass]
public abstract partial class BT_ConditionNode : BT_Node
{
    protected abstract bool CheckCondition();

    public override NodeState Tick()
    {
        return CheckCondition() ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}