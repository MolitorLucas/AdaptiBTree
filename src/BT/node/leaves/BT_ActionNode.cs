using Godot;
using System;
using System.Threading.Tasks;

[GlobalClass]
public abstract partial class BT_ActionExecutor : Godot.Node
{
    public abstract Task<NodeState> Execute();
}

[GlobalClass]
public partial class BT_ActionNode : BT_Node
{
    [Export]
    public BT_ActionExecutor ActionExecutor { get; set; }

    private Task<NodeState> _currentTask;
    private NodeState _lastResult = NodeState.RUNNING;

    public override NodeState Tick()
    {
        if (_currentTask == null)
        {
            _currentTask = ActionExecutor.Execute();
            _lastResult = NodeState.RUNNING;
        }

        if (_currentTask.IsCompleted)
        {
            if (_currentTask.IsCompletedSuccessfully)
            {
                _lastResult = _currentTask.Result;
            }
            else
            {
                GD.PrintErr("Action execution failed with an exception: " + _currentTask.Exception);
                _lastResult = NodeState.FAILURE;
            }
            _currentTask = null;
        }

        return _lastResult;
    }
}
