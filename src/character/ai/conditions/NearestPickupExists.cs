using Godot;
using System.Linq;

public partial class NearestPickupExists : BT_ConditionNode
{
    [Export]
    public float SearchRadius { get; set; } = 300f;

    protected override bool CheckCondition(Node actor, Blackboard blackboard)
    {
        if (actor is not Node2D actor2d) return false;
        var pickups = actor.GetTree().GetNodesInGroup("pickups").OfType<Area2D>();
        return pickups.Any(p => p.GlobalPosition.DistanceTo(actor2d.GlobalPosition) <= SearchRadius);
    }
}
