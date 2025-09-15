using Godot;
using System.Threading.Tasks;

public partial class Attack : BT_ActionNode
{

    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Agent agent) return Task.FromResult(NodeState.FAILURE);
        var target = actor.Get("Opponent").As<Node2D>();
        if (target == null) return Task.FromResult(NodeState.FAILURE);
        var stats = agent.GetNodeOrNull<CharacterStats>("CharacterStats");
        var otherStats = target.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (stats == null || otherStats == null) return Task.FromResult(NodeState.FAILURE);

        if (!stats.HasSpecialPower) return Task.FromResult(NodeState.FAILURE);

        agent.GlobalPosition = agent.GlobalPosition.MoveToward(target.GlobalPosition, (float) GetPhysicsProcessDeltaTime() * blackboard["CharacterSpeed"].As<float>());

        return Task.FromResult(NodeState.SUCCESS);
    }
}
