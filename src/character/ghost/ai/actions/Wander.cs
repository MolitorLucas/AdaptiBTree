using Godot;
using System;
using System.Threading.Tasks;

public partial class Wander : BT_ActionNode
{
    [Export]
    public float Radius { get; set; } = 64f;

    private float _timeSinceLastSet = 0f;

    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D node)
            return Task.FromResult(NodeState.FAILURE);

        var delta = (float)actor.GetPhysicsProcessDeltaTime();
        _timeSinceLastSet += delta;
        if (!blackboard.HasKey("TargetPosition") || _timeSinceLastSet > 2f)
        {
            var rand = new Random();
            var angle = (float)(rand.NextDouble() * Math.PI * 2.0);
            var offset = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * Radius;
            var target = node.GlobalPosition + offset;
            blackboard.SetValue("TargetPosition", target);
            _timeSinceLastSet = 0f;
            return Task.FromResult(NodeState.SUCCESS);
        }

        return Task.FromResult(NodeState.RUNNING);
    }
}
