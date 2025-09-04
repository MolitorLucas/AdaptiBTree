using Godot;
using System;

[GlobalClass]
public partial class BT_ConditionNode : BT_Node
{
    protected virtual bool CheckCondition(Node2D actor, Blackboard blackboard)
    {
        GD.PrintErr("CheckCondition method not implemented in " + GetType().Name);
        return false;
    }

    public override NodeState Tick(Node2D actor, Blackboard blackboard)
    {
        return CheckCondition(actor, blackboard) ? NodeState.SUCCESS : NodeState.FAILURE;
    }
}