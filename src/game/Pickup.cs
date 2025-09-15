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
        Connect(Area2D.SignalName.BodyEntered, new Callable(this, nameof(OnBodyEntered)));
        AddToGroup("pickups");
    }

    private void OnBodyEntered(CharacterBody2D body)
    {
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
                    if (stats.HasSpecialPower)
                    {
                        stats.BuffAttack(stats.AttackPower / 2, 2.5f);
                    }
                    else {
                        stats.GrantSpecial(5.0f);
                    }
                }
                QueueFree();
            }
        }
    }
}
