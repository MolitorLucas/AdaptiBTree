using Godot;
using System;

[GlobalClass]
public partial class CharacterStats : Node
{
    [Export]
    public int Points { get; private set; } = 0;

    public bool HasSpecialPower { get => _specialRemaining > 0f; }

    public int AttackPower { get; private set; } = 5;
    private float _specialRemaining = 0f;

    public override void _Process(double delta)
    {
        if (HasSpecialPower)
        {
            _specialRemaining -= (float)delta;
            if (_specialRemaining <= 0)
            {
                _specialRemaining = 0f;
                AttackPower = 5;
            }
        }
    }

    public void AddPoints(int amount)
    {
        Points += amount;
    }

    public void GrantSpecial(float duration)
    {
        _specialRemaining = duration;
    }

    public void BuffAttack(int amount = 5, float durationAmount = 0f)
    {
        AttackPower += amount;
        _specialRemaining += durationAmount;
    }

    public int StealPointsFrom(CharacterStats other, int amount = -1)
    {
        if (other == null) return 0;
        int stolen = Math.Min(amount > 0 ? amount : AttackPower, other.Points);
        other.Points -= stolen;
        Points += stolen;
        return stolen;
    }
}
