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
        if (actor is not Agent agent) return;
        CharacterStats opponentStats = agent.Opponent?.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (opponentStats == null || opponentStats.HasSpecialPower)
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
        if (actor is not Agent agent) return;
        CharacterStats opponentStats = agent.Opponent?.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (opponentStats == null || !opponentStats.HasSpecialPower)
        {
            return;
        }
        BT_Node subtree = GD.Load<PackedScene>("res://src/character/ai/subtrees/FleeingSubTree.tscn").Instantiate<BT_Node>();
        blackboard["fleeingSubTree"] = subtree.GetPath();
        BT_ReflectionHelper.InsertSubTreeBelow(actor.GetNode<BT_Tree>("BehaviorTree").GetChild<BT_Node>(0), subtree);
    }


}
