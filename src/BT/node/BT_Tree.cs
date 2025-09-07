using Godot;
using System;

[GlobalClass]
public partial class BT_Tree : Node
{
    public bool Active = true;
    public Blackboard Blackboard { get; set; } = new Blackboard();
    public NodeState CurrentState { get; set; } = NodeState.INVALID;

    public NodeState Tick(Node actor)
    {
        var response = NodeState.RUNNING;
        foreach (var child in GetChildren())
        {
            var node= child as BT_Node;
            node.CurrentState = node.Tick(actor, Blackboard);
        }
        CurrentState = response;
        return response;
    }
}
