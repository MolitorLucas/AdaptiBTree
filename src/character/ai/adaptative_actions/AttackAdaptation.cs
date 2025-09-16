using Godot;
using System;

public partial class AttackAdaptation : BT_AdaptiveNode
{
    public override void BeforeAdaptativeAction(Node actor, Blackboard blackboard = null)
    {
        if (!blackboard.HasKey("attackSubTree"))
        {
            return;
        }
        if (actor is not Agent agent) return;
        CharacterStats agentStats = agent.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (agentStats == null || agentStats.HasSpecialPower)
        {
            return;
        }
        string subTreeRootNode = (string)blackboard["attackSubTree"];
        BT_ReflectionHelper.RemoveSubtree(GetChild<BT_Node>(0), GetNode<BT_Node>($"{subTreeRootNode}"));
        blackboard.RemoveKey("attackSubTree");
    }

    public override void AfterAdaptativeAction(Node actor, Blackboard blackboard = null)
    {
        if (blackboard.HasKey("attackSubTree"))
        {
            return;
        }
        if (actor is not Agent agent) return;
        CharacterStats agentStats = agent.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (agentStats == null || !agentStats.HasSpecialPower)
        {
            return;
        }
        BT_Node subtree = GD.Load<PackedScene>("res://src/character/ai/subtrees/AttackSubTree.tscn").Instantiate<BT_Node>();
        BT_Node parent = GetChild<BT_Node>(0);
        BT_ReflectionHelper.InsertSubTreeBelow(parent, subtree);
        parent.MoveChild(subtree, 0);
        subtree.UniqueNameInOwner = true;
        blackboard["attackSubTree"] = subtree.GetPath(); 
    }
}
