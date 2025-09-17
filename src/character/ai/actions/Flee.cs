using Godot;
using System;
using System.Threading.Tasks;

public partial class Flee : BT_ActionNode
{
    public override Task<NodeState> Execute(Node actor, Blackboard blackboard)
    {
        if (actor is not Agent agent)
            return Task.FromResult(NodeState.FAILURE);

        var speed = blackboard["CharacterSpeed"].As<float>()*1.05f;
        var delta = (float)agent.GetPhysicsProcessDeltaTime();
        CharacterBody2D agentBody = agent.GetNodeOrNull<CharacterBody2D>("CharacterBody2D");
        agentBody.SetVelocity(agent.GlobalPosition.DirectionTo(agent.Opponent.GlobalPosition) * -speed * delta);
        agentBody.MoveAndSlide();
        agent.GlobalPosition = agentBody.GlobalPosition;

        return Task.FromResult(NodeState.SUCCESS);
    }
}