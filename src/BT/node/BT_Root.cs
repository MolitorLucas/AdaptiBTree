using Godot;
using System;

[GlobalClass]
public partial class BT_Root : BT_Node
{
    public bool Active = true;
    public Blackboard Blackboard { get; set; } = new Blackboard();

    public BT_Root()
    {
        CurrentState = NodeState.RUNNING;
    }
    public override NodeState Tick(Node2D actor, Blackboard blackboard = null)
    {
        var response = NodeState.RUNNING;
        foreach (var child in GetChildren())
        {
            if (child is BT_Node node)
            {
                node.CurrentState = node.Tick(actor, Blackboard);
            }
        }
        CurrentState = response;
        return response;
    }
}
