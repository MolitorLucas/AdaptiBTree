using Godot;
using System;
using System.Linq;
using System.Threading.Tasks;

public partial class HudDisplay : Control
{
    [Export]
    public Agent Agent { get; set; }

    private BT_Tree Tree => Agent?.GetNode<BT_Tree>("BehaviorTree");

    private VBoxContainer _treeContainer;

    private Label pointsLabel = new()
    {
        Name = "PointsLabel",
    };

    public override void _Ready()
    {
        _treeContainer = new VBoxContainer();
        AddChild(_treeContainer);
        UpdateTree();
        UpdateAgentPoints();
    }

    public override void _Process(double delta)
    {
        UpdateTree();
        UpdateAgentPoints();
    }

    private void UpdateTree()
    {
        foreach (var child in _treeContainer.GetChildren())
        {
            if (child is Label label)
            {
                _treeContainer.RemoveChild(label);
            }
        }
        if (Tree != null)
        {
            Tree.Tick(Tree.GetParent<Node2D>());
            AddNodeToUI(Tree, _treeContainer, 0);
        }    
    }

    private void UpdateAgentPoints()
    {
        if (Agent == null) return;

        pointsLabel.Modulate = Agent.GetNode("Sprite2D") is Sprite2D sprite ? sprite.Modulate : Colors.White;
        pointsLabel.Text = $"Points: {Agent.GetNodeOrNull<CharacterStats>("CharacterStats")?.Points ?? 0}";
        _treeContainer.AddChild(pointsLabel);
    }

    private void AddNodeToUI(BT_Node node, VBoxContainer parent, int indent)
    {
        var label = new Label
        {
            Text = $"{string.Concat(Enumerable.Repeat("|     ", indent))}{node.Name}: {node.CurrentState}",
            Modulate = GetColorForState(node.CurrentState)
        };
        parent.AddChild(label);

        foreach (var child in node.GetChildren())
        {
            if (child is BT_Node btChild)
                AddNodeToUI(btChild, parent, indent + 1);
        }
    }
    
    private void AddNodeToUI(BT_Tree node, VBoxContainer parent, int indent)
    {
        var label = new Label
        {
            Text = $"{new string(' ', indent * 6)}{node.GetType().Name}: {node.CurrentState}",
            Modulate = GetColorForState(node.CurrentState)
        };
        
        parent.AddChild(label);

        foreach (var child in node.GetChildren())
        {
            if (child is BT_Node btChild)
                AddNodeToUI(btChild, parent, indent + 1);
        }
    }


    private Color GetColorForState(NodeState state)
    {
        return state switch
        {
            NodeState.SUCCESS => Colors.Green,
            NodeState.FAILURE => Colors.Red,
            NodeState.RUNNING => Colors.Yellow,
            _ => Colors.Gray
        };
    }
}
