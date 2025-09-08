using Godot;
using System;

public partial class AttackAdaptation : BT_AdaptiveNode
{
    public override void BeforeAdaptativeAction(Node actor, Blackboard blackboard = null)
    {
        if (blackboard == null) return;
        if (!blackboard.HasKey("attackSubTree"))
        {
            return;
        }
        string subTreeRootNode = (string)blackboard["attackSubTree"];
        BT_ReflectionHelper.RemoveSubtree(actor.GetNode<BT_Tree>("BehaviorTree"), actor.GetNode<BT_Node>(subTreeRootNode));
        blackboard.RemoveKey("attackSubTree");
    }

    public override void AfterAdaptativeAction(Node actor, Blackboard blackboard = null)
    {
        if (blackboard == null) return;
        if (blackboard.HasKey("attackSubTree"))
        {
            return;
        }
        
        var stats = actor.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (stats == null || !stats.HasSpecialPower)
        {
            return;
        }

        BT_Node subtree = GD.Load<PackedScene>("res://src/character/ai/subtrees/AttackSubTree.tscn").Instantiate<BT_Node>();
        blackboard["attackSubTree"] = subtree.GetPath();
        BT_ReflectionHelper.InsertSubTreeAbove(actor.GetNode<BT_Tree>("BehaviorTree").GetChild<BT_Node>(0), subtree);
    }
}
