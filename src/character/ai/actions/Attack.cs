using Godot;
using System.Threading.Tasks;

public partial class Attack : BT_ActionNode
{
    [Export]
    public int Amount { get; set; } = 5;

    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Agent agent) return Task.FromResult(NodeState.FAILURE);
        var target = actor.Get("Opponent").As<Node2D>();
        if (target == null) return Task.FromResult(NodeState.FAILURE);
        var stats = agent.GetNodeOrNull<CharacterStats>("CharacterStats");
        var otherStats = target.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (stats == null || otherStats == null) return Task.FromResult(NodeState.FAILURE);

        if (!stats.HasSpecialPower) return Task.FromResult(NodeState.FAILURE);

        if (agent.GlobalPosition.DistanceTo(target.GlobalPosition) <= 24)
        {
            stats.StealPointsFrom(otherStats, Amount);
            stats.GrantSpecial(0);
            return Task.FromResult(NodeState.SUCCESS);
        }

        return Task.FromResult(NodeState.FAILURE);
    }
}
