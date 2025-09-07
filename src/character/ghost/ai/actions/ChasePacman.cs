using Godot;
using System.Threading.Tasks;

public partial class ChasePacman : BT_ActionNode
{
    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (!blackboard.HasKey("Pacman"))
            return Task.FromResult(NodeState.FAILURE);

        var pacman = blackboard.GetValue("Pacman").As<Node2D>();
        if (pacman == null) return Task.FromResult(NodeState.FAILURE);

        // Set target to pacman position
        blackboard.SetValue("TargetPosition", pacman.GlobalPosition);
        return Task.FromResult(NodeState.SUCCESS);
    }
}
