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
        BT_ReflectionHelper.RemoveSubtree(actor as BT_Tree, actor as BT_Node);
        

    }

}
