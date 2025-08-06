using Godot;
using System;

[GlobalClass]
public abstract partial class BT_Node : Node
{
    public NodeState CurrentState { get; set; } = NodeState.IDLE;

    public abstract NodeState Tick();

}
