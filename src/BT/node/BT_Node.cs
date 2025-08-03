using Godot;
using System;

[GlobalClass]
public abstract partial class BT_Node : Node
{
    public virtual NodeState CurrentState { get; protected set; } = NodeState.RUNNING;
    public abstract NodeState Tick();

}
