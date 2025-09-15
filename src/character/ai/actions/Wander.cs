using Godot;
using System.Threading.Tasks;

public partial class Wander : BT_ActionNode
{
    private Vector2 _target;
    private RandomNumberGenerator _rng = new();

    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D actor2d) return Task.FromResult(NodeState.FAILURE);
        if (actor2d.GlobalPosition.DistanceTo(_target) < 24 || actor2d.GlobalPosition.DistanceTo(_target) > 400)
        {
            var size = actor2d.GetViewport().GetVisibleRect().Size;
            var x = _rng.RandfRange(50, size.X + 50);
            var y = _rng.RandfRange(50, size.Y + 50);
            _target = new Vector2(x, y);
        }

        var speed = blackboard["CharacterSpeed"].As<float>();
        var delta = (float)actor2d.GetPhysicsProcessDeltaTime();
        actor2d.GlobalPosition = actor2d.GlobalPosition.MoveToward(_target, speed * delta);
        return Task.FromResult(NodeState.SUCCESS);
    }
}
