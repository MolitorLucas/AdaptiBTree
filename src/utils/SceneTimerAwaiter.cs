using Godot;
using System;

public static class SceneTimerAwaiter
{
    public static async void AwaitSceneTimer(this Node node, float seconds)
    {
        SceneTreeTimer timer = node.GetTree().CreateTimer(seconds);
        await node.ToSignal(timer, SceneTreeTimer.SignalName.Timeout);
    }
}
