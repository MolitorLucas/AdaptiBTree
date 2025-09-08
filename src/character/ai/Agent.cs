using Godot;
using System;
[GlobalClass]
public partial class Agent : Node2D
{
    [Export]
    public Node2D Opponent { get; set; }
}
