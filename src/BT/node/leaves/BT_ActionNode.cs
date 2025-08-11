using Godot;
using System;
using System.Threading.Tasks;

[GlobalClass]
public partial class BT_ActionNode : BT_Node
{
    public virtual Task<NodeState> Execute(Node2D actor, Blackboard blackboard)
    {
        GD.PrintErr("Execute method not implemented in " + GetType().Name);
        return Task.FromResult(NodeState.FAILURE);
    }

    protected Task<NodeState> _currentTask;
    protected NodeState _lastResult = NodeState.IDLE;

    public override NodeState Tick(Node2D actor, Blackboard blackboard)
    {
        if (_currentTask == null)
        {
            _currentTask = Execute(actor, blackboard);
            _lastResult = NodeState.RUNNING;
            return _lastResult;
        }

        if (!_currentTask.IsCompleted)
        {
            return NodeState.RUNNING;
        }

        if (_currentTask.IsCompletedSuccessfully)
        {
            _lastResult = _currentTask.Result;
        }
        else
        {
            GD.PrintErr("Action execution failed with an exception: " + _currentTask.Exception);
            _lastResult = NodeState.FAILURE;
        }
        var result = _lastResult;
        _currentTask = null;
        _lastResult = NodeState.IDLE; 
        return result;
    }
}
