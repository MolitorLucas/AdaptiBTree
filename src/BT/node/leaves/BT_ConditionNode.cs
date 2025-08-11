using Godot;
using System;

[GlobalClass]
public partial class BT_ConditionNode : BT_Node
{
    protected virtual bool CheckCondition()
    {
        GD.PrintErr("CheckCondition method not implemented in " + GetType().Name);
        return false;
    }

    public override NodeState Tick(Node2D actor, Blackboard blackboard)
    {
        CurrentState = CheckCondition() ? NodeState.SUCCESS : NodeState.FAILURE;
        return CurrentState;
    }
}