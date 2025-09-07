using Godot;
using System;

public partial class FleeingAdaptation : BT_AdaptiveNode
{
    public override void BeforeAdaptativeAction(Node actor, Blackboard blackboard)
    {
        if (!blackboard.HasKey("fleeingSubTree"))
        {
            return;
        }
        string subTreeRootNode = (string)blackboard["fleeingSubTree"];
        BT_ReflectionHelper.RemoveSubtree(actor.GetNode<BT_Tree>("BehaviorTree"), GetNode<BT_Node>(subTreeRootNode));
        blackboard.RemoveKey("fleeingSubTree");
    }

    public override void AfterAdaptativeAction(Node actor, Blackboard blackboard = null)
    {
        if (blackboard.HasKey("fleeingSubTree"))
        {
            return;
        }
        BT_Node subtree = GD.Load<PackedScene>("res://src/character/ai/subtrees/FleeingSubTree.tscn").Instantiate<BT_Node>();
        blackboard["fleeingSubTree"] = subtree.GetPath();
        BT_ReflectionHelper.InsertSubTreeAbove(actor.GetNode<BT_Tree>("BehaviorTree").GetChild<BT_Node>(0), subtree);
    }


}
