using Godot;
using System;

[GlobalClass]
public partial class GhostAI : Node2D
{
    private BT_Tree _tree;
    public Blackboard Blackboard { get; private set; } = new Blackboard();

    [Export]
    public float CharacterSpeed { get; set; } = 100f;
    [Export]
    public float DetectionDistance { get; set; } = 160f;
    [Export]
    public NodePath PacmanPath { get; set; }

    public Node2D PacmanNode { get; set; }

    public override void _Ready()
    {
        base._Ready();

        Blackboard.SetValue("CharacterSpeed", CharacterSpeed);
        Blackboard.SetValue("DetectionDistance", DetectionDistance);
        Blackboard.SetValue("DistanceThreshold", 8f);

        if (PacmanPath != null && HasNode(PacmanPath))
            PacmanNode = GetNode<Node2D>(PacmanPath);

        _tree = new BT_Tree();

        var selector = new BT_SelectorNode();

        var chaseSeq = new BT_SequenceNode();
        chaseSeq.AddChild(new IsPacmanNearby());
        chaseSeq.AddChild(new ChasePacman());
        chaseSeq.AddChild(new MoveToTarget());

        var wanderSeq = new BT_SequenceNode();
        wanderSeq.AddChild(new Wander());
        wanderSeq.AddChild(new MoveToTarget());

        selector.AddChild(chaseSeq);
        selector.AddChild(wanderSeq);

        _tree.AddChild(selector);
        AddChild(_tree);
    }

    public override void _PhysicsProcess(double delta)
    {
        if (PacmanNode == null)
        {
            if (PacmanPath != null && HasNode(PacmanPath))
                PacmanNode = GetNode<Node2D>(PacmanPath);
            else
            {
                // fallback: find first node in group "Pacman"
                foreach (var n in GetTree().Root.GetChildren())
                {
                    if (n is Node node && node.IsInGroup("Pacman"))
                    {
                        PacmanNode = node as Node2D;
                        break;
                    }
                }
            }
        }

        Blackboard.SetValue("Pacman", PacmanNode);
        _tree.Blackboard = Blackboard;
        _tree.Tick(this);
    }
}
