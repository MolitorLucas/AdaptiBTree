using Godot;
using System;

public partial class Simulation : Node2D
{
    [Export]
    public PackedScene PickupScene { get; set; }
    [Export]
    public PackedScene SpecialPickupScene { get; set; }

    public override void _Ready()
    {
        var agentA = GetNodeOrNull<Agent>("AgentA");
        var agentB = GetNodeOrNull<Agent>("AgentB");

        if (agentA != null && agentB != null)
        {
            agentA.Opponent = agentB;
            agentB.Opponent = agentA;
        }
        var controller = GetNode<GameController>("GameController");
        AddChild(controller);

        controller.PickupScene = GD.Load<PackedScene>("res://src/game/Pickup.tscn");
        controller.SpecialPickupScene = GD.Load<PackedScene>("res://src/game/SpecialPickup.tscn");

        controller.AgentA = GetNode<Agent>("AgentA");
        controller.AgentB = GetNode<Agent>("AgentB");
    }
}
