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
        Connect("body_entered", new Callable(this, nameof(OnBodyEntered)));
        AddToGroup("pickups");
    }

    private void OnBodyEntered(Node body)
    {
        if (body is Node2D node)
        {
            var stats = node.GetNodeOrNull<CharacterStats>("CharacterStats");
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
