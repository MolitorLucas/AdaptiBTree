using Godot;
using System;

public partial class BT_Root : BT_Node
{
    public bool Active = true;

    public override NodeState Tick()
    {
        if(!Active)
        {
            return NodeState.SUCCESS;
        }
        foreach (var child in GetChildren())
        {
            if (child is BT_Node node)
            {
                node.Tick();
            }
        }
        return Tick();
    }
}
