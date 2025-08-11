using Godot;
using System;
using System.Threading.Tasks;

public partial class Main : Control
{
    [Export]
    public BT_Node RootNode { get; set; }

    public Blackboard Blackboard { get; set; } = new Blackboard();

    private VBoxContainer _treeContainer;

    public override void _Ready()
    {
        _treeContainer = new VBoxContainer();
        AddChild(_treeContainer);
        UpdateTree();
    }

    public override void _Process(double delta)
    {
        UpdateTree();
    }

    private void UpdateTree()
    {
        Blackboard.SetValue("character_speed", 200);
        foreach (var child in _treeContainer.GetChildren())
        {
            if (child is Label label)
                _treeContainer.RemoveChild(label);
        }
        if (RootNode != null)
        {
            RootNode.Tick(RootNode.GetParent<Node2D>(), Blackboard); 
            AddNodeToUI(RootNode, _treeContainer, 0);
        }
            
            
    }

    private void AddNodeToUI(BT_Node node, VBoxContainer parent, int indent)
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
