using Godot;
using System;

public partial class Player : Node2D
{

    [Export]
    public BT_Tree BehaviorTree;
    public override void _Ready()
    {
        BehaviorTree.Blackboard["CharacterSpeed"] = 200f;
        GD.Print("Player ready");
    }

    public override void _Process(double delta)
    {
        
    }
}
