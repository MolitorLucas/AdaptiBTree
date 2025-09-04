using Godot;
using System;

public partial class FleeingAdaptation : BT_AdaptiveNode
{
    public override void BeforeAdaptativeAction(Node2D actor, Blackboard blackboard)
    {
        if (!blackboard.HasKey("fleeingSubTree"))
        {
            return;
        }

        string subTreeRootNode = (string)blackboard["fleeingSubTree"];
        BT_ReflectionHelper.RemoveSubtree(subTreeRootNode,);
        

    }

}
