using Godot;
using System;

[GlobalClass]
public partial class BT_Root : BT_Node
{
    public bool Active = true;

    public override NodeState Tick()
    {
        var response = NodeState.RUNNING;
        foreach (var child in GetChildren())
        {
            if (child is BT_Node node)
            {
                node.CurrentState = node.Tick();
            }
        }
        CurrentState = response;
        return response;
    }
}
