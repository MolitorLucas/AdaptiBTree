using Godot;
using System;

[GlobalClass]
public partial class MovementController : CharacterBody2D
{
    public const float Speed = 300.0f;
    public const float JumpVelocity = -400.0f;

    public Vector2 Direction { get; set; } = Vector2.Zero;

    public override void _PhysicsProcess(double delta)
    {
        Vector2 velocity = Velocity;
        if (Direction != Vector2.Zero)
        {
            velocity.X = Direction.X * Speed;
            velocity.Y = Direction.Y * Speed;
        }
        else
        {
            velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
            velocity.Y = Mathf.MoveToward(Velocity.Y, 0, Speed);
        }

        Velocity = velocity;
        MoveAndSlide();
    }

    public void DirectionEventData(string eventData)
    {
        if (eventData == "right")
        {
            Direction = new Vector2(1, 0);
        }
        else if (eventData == "left")
        {
            Direction = new Vector2(-1, 0);
        }
        else if (eventData == "up")
        {
            Direction = new Vector2(0, -1);
        }
        else if (eventData == "down")
        {
            Direction = new Vector2(0, 1);
        }
    }
}
