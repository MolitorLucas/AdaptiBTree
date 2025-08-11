using Godot;
using System;

[GlobalClass]
public partial class BT_Root : BT_Node
{
    public bool Active = true;

    public override NodeState Tick(Node2D actor, Blackboard blackboard)
    {
        var response = NodeState.RUNNING;
        foreach (var child in GetChildren())
        {
            if (child is BT_Node node)
            {
                node.CurrentState = node.Tick(actor, blackboard);
            }
        }
        CurrentState = response;
        return response;
    }
}
