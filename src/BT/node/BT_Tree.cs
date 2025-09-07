using Godot;
using System;

[GlobalClass]
public partial class BT_Tree : Node
{
    public bool Active = true;
    [Export]
    public Blackboard Blackboard { get; set; }
    public NodeState CurrentState { get; set; } = NodeState.INVALID;

    public NodeState Tick(Node actor)
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
