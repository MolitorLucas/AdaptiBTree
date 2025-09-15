using Godot;
using System;
using System.Collections.Generic;
using System.Linq;

public partial class GameController : Node2D
{
    [Export]
    public PackedScene PickupScene { get; set; }

    [Export]
    public PackedScene SpecialPickupScene { get; set; }

    [Export]
    public Agent AgentA { get => _agentA;  set => _agentA = value; }

    [Export]
    public Agent AgentB { get => _agentB; set => _agentB = value; }

    private Agent _agentA;
    private Agent _agentB;

    private RandomNumberGenerator _rng = new();

    private float _spawnTimer = 0f;
    private float _spawnInterval = 1.0f;

    public override void _Ready()
    {
        _rng.Randomize();
    }

    public override void _Process(double delta)
    {
        _spawnTimer += (float)delta;
        if (_spawnTimer >= _spawnInterval)
        {
            _spawnTimer = 0f;
            SpawnPickup();
        }
        FindBestPickups();
        CheckSpecialCollisions();
        CheckWinCondition();
    }

    private void SpawnPickup()
    {
        var isSpecial = _rng.RandiRange(0, 10) == 0;
        var scene = isSpecial ? SpecialPickupScene : PickupScene;
        if (scene == null) return;
        var instance = scene.Instantiate<Area2D>();
        var x = _rng.RandfRange(50, GetViewportRect().Size.X - 50);
        var y = _rng.RandfRange(50, GetViewportRect().Size.Y - 50);
        instance.Position = new Vector2(x, y);
        AddChild(instance);
    }

    private void FindBestPickups()
    {
        var pickups = GetTree().GetNodesInGroup("pickups").OfType<Area2D>();
        if (!pickups.Any()) return;

        var agents = new List<Agent> { _agentA, _agentB };
        foreach (var agent in agents)
        {
            if (agent == null) continue;
            var blackboard = agent.GetNodeOrNull<BT_Tree>("BehaviorTree")?.Blackboard;
            if (blackboard == null) continue;

            float best = pickups.First().GlobalPosition.DistanceTo(agent.GlobalPosition);
            Area2D nearest = pickups.First();
            foreach (var p in pickups.Skip(1))
            {
                var dist = p.GlobalPosition.DistanceTo(agent.GlobalPosition);
                if (dist < best)
                {
                    best = dist;
                    nearest = p;
                }
            }
            blackboard["BestPickupFor" + agent.Name] = nearest;
        }
    }

    private void CheckSpecialCollisions()
    {
        var statsA = _agentA.GetNodeOrNull<CharacterStats>("CharacterStats");
        var statsB = _agentB.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (statsA == null || statsB == null) return;

        if (statsA.HasSpecialPower)
        {
            if (_agentA.GlobalPosition.DistanceTo(_agentB.GlobalPosition) < 24)
            {
                statsA.StealPointsFrom(statsB);
                statsA.GrantSpecial(0);
            }
        }
        if (statsB.HasSpecialPower)
        {
            if (_agentB.GlobalPosition.DistanceTo(_agentA.GlobalPosition) < 24)
            {
                statsB.StealPointsFrom(statsA);
                statsB.GrantSpecial(0);
            }
        }
    }

    private void CheckWinCondition()
    {
        var statsA = _agentA.GetNodeOrNull<CharacterStats>("CharacterStats");
        var statsB = _agentB.GetNodeOrNull<CharacterStats>("CharacterStats");
        if (statsA == null || statsB == null) return;

        if (statsA.Points >= 100)
        {
            GetTree().Paused = true;
        }
        else if (statsB.Points >= 100)
        {
            GetTree().Paused = true;
        }
    }
}
