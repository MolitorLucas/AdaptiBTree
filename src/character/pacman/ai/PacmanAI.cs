using Godot;
using System;

[GlobalClass]
public partial class PacmanAI : Node2D
{
    private BT_Tree _tree;
    public Blackboard Blackboard { get; private set; } = new Blackboard();

    [Export]
    public float CharacterSpeed { get; set; } = 120f;
    [Export]
    public float FearDistance { get; set; } = 100f;
    [Export]
    public string FoodGroup { get; set; } = "Food";
    [Export]
    public string GhostGroup { get; set; } = "Ghost";

    public override void _Ready()
    {
        base._Ready();

        // Setup blackboard defaults
        Blackboard.SetValue("CharacterSpeed", CharacterSpeed);
        Blackboard.SetValue("FearDistance", FearDistance);
        Blackboard.SetValue("DistanceThreshold", 8f);

        // Create tree root
        _tree = new BT_Tree();

        // Build nodes
        var selector = new BT_SelectorNode();

        // Flee sequence
        var fleeSeq = new BT_SequenceNode();
        var isGhostNearby = new IsGhostNearby();
        var computeFleeTarget = new ComputeFleeTarget();
        var moveTo = new MoveToTarget();

        // Seek food sequence
        var seekSeq = new BT_SequenceNode();
        var findFood = new FindNearestFood() { FoodGroupName = FoodGroup };
        var moveToFood = new MoveToTarget();

        fleeSeq.AddChild(isGhostNearby);
        fleeSeq.AddChild(computeFleeTarget);
        fleeSeq.AddChild(moveTo);

        seekSeq.AddChild(findFood);
        seekSeq.AddChild(moveToFood);

        selector.AddChild(fleeSeq);
        selector.AddChild(seekSeq);

        _tree.AddChild(selector);
        AddChild(_tree);
    }

    public override void _PhysicsProcess(double delta)
    {
        // Update dynamic blackboard entries
        var ghosts = new Godot.Collections.Array();
        foreach (var n in GetTree().Root.GetChildren())
        {
            if (n is Node2D node && node.IsInGroup(GhostGroup))
                ghosts.Add(node);
        }
        Blackboard.SetValue("Ghosts", ghosts);

        _tree.Blackboard = Blackboard;
        _tree.Tick(this);
    }
}
