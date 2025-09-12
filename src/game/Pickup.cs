using Godot;
using System;

public partial class Pickup : Area2D
{
    [Export]
    public int Value { get; set; } = 1;

    [Export]
    public bool IsSpecial { get; set; } = false;

    public override void _Ready()
    {
        Connect(SignalName.BodyEntered, new Callable(this, nameof(OnBodyEntered)));
        AddToGroup("pickups");
    }

    private void OnBodyEntered(CharacterBody2D body)
    {
        GD.Print("Pickup collected by: " + body.Name);
        if (body is CharacterBody2D node)
        {
            Agent agent = node.GetParent<Agent>();
            if (agent == null) return;
            var stats = agent.GetNodeOrNull<CharacterStats>("CharacterStats");
            if (stats != null)
            {
                stats.AddPoints(Value);
                if (IsSpecial)
                {
                    stats.GrantSpecial(5.0f);
                }
                QueueFree();
            }
        }
    }
}
