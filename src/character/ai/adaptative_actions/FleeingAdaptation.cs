using Godot;
using System;

public partial class FleeingAdaptation : BT_AdaptiveNode
{
    public override void BeforeAdaptativeAction(Node actor, Blackboard blackboard = null)
    {
        if (!blackboard.HasKey("fleeingSubTree"))
        {
            return;
        }
        if (actor is not Agent agent) return;
        CharacterStats opponentStats = agent.Opponent.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (opponentStats == null || opponentStats.HasSpecialPower)
        {
            return;
        }
        string subTreeRootNode = (string)blackboard["fleeingSubTree"];
        BT_ReflectionHelper.RemoveSubtree(GetChild<BT_Node>(0).GetChild<BT_Node>(0), GetNode<BT_Node>($"{subTreeRootNode}"));
        blackboard.RemoveKey("fleeingSubTree");
    }

    public override void AfterAdaptativeAction(Node actor, Blackboard blackboard = null)
    {
        if (blackboard.HasKey("fleeingSubTree"))
        {
            return;
        }
        if (actor is not Agent agent) return;
        CharacterStats opponentStats = agent.Opponent.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (opponentStats == null || !opponentStats.HasSpecialPower)
        {
            return;
        }
        BT_Node subtree = GD.Load<PackedScene>("res://src/character/ai/subtrees/FleeingSubTree.tscn").Instantiate<BT_Node>();
        BT_Node parent = GetChild<BT_Node>(0).GetChild<BT_Node>(0);
        BT_ReflectionHelper.InsertSubTreeBelow(parent, subtree);
        parent.MoveChild(subtree, 0);
        subtree.UniqueNameInOwner = true;
        blackboard["fleeingSubTree"] = subtree.GetPath();
    }


}
