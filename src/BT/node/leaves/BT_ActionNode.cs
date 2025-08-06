using Godot;
using System;
using System.Threading.Tasks;

[GlobalClass]
public abstract partial class BT_ActionExecutor : Godot.Node
{
    public abstract Task<NodeState> Execute();
}
public partial class TestActionExecutor : BT_ActionExecutor
{
    public override async Task<NodeState> Execute()
    {
        this.AwaitSceneTimer(1.0f);
        var result = await Task.Run(
            () =>
            {

                var possibleResults = new[] { NodeState.SUCCESS, NodeState.FAILURE, NodeState.RUNNING };
                RandomNumberGenerator randomNumberGenerator = new RandomNumberGenerator();
                randomNumberGenerator.Randomize();
                var chosen = randomNumberGenerator.RandiRange(0, possibleResults.Length - 1);
                return possibleResults[chosen];
            }
        );
        return result;
    }
}
[GlobalClass]
public partial class BT_ActionNode : BT_Node
{
    [Export]
    public BT_ActionExecutor ActionExecutor { get; set; }

    private Task<NodeState> _currentTask;
    private NodeState _lastResult = NodeState.RUNNING;

    public override void _Ready()
    {
        ActionExecutor ??= new TestActionExecutor();
        AddChild(ActionExecutor);
    }

    public override NodeState Tick()
    {
        if (_currentTask == null)
        {
            _currentTask = ActionExecutor.Execute();
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
        return result;
    }
}
