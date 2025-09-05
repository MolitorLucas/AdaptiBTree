using Godot;
using System;

[GlobalClass]
public abstract partial class BT_Node : Node
{
    public bool IsActive = false;
    private NodeState _currentState = NodeState.INVALID;
    public NodeState CurrentState
    {
        get => _currentState;
        set
        {
            IsActive = value != NodeState.RUNNING;
            _currentState = value;
        }
    }

    public abstract NodeState Tick(Node actor, Blackboard blackboard);

}
