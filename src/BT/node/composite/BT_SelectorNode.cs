using Godot;
using System;
using System.Threading.Tasks;

[GlobalClass]
public partial class BT_SelectorNode : BT_Node
{
    public override NodeState Tick()
    {
        foreach (var child in GetChildren())
        {
            this.AwaitSceneTimer(1.0f);
            if (child is BT_Node node)
            {
                node.CurrentState = node.Tick();
                if (node.CurrentState == NodeState.SUCCESS)
                    return NodeState.SUCCESS;
                if (node.CurrentState == NodeState.RUNNING)
                    return NodeState.RUNNING;
            }
        }

        return NodeState.FAILURE;
    }
}
