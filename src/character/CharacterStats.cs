using Godot;
using System;

[GlobalClass]
public partial class CharacterStats : Node2D
{
    [Export]
    public int Points { get; private set; } = 0;

    public bool HasSpecialPower { get; private set; } = false;
    private float _specialRemaining = 0f;

    public override void _Process(double delta)
    {
        if (HasSpecialPower)
        {
            _specialRemaining -= (float)delta;
            if (_specialRemaining <= 0)
            {
                HasSpecialPower = false;
                _specialRemaining = 0f;
            }
        }
    }

    public void AddPoints(int amount)
    {
        Points += amount;
    }

    public void GrantSpecial(float duration)
    {
        HasSpecialPower = true;
        _specialRemaining = duration;
    }

    public int StealPointsFrom(CharacterStats other, int amount)
    {
        if (other == null) return 0;
        int stolen = Math.Min(amount, other.Points);
        other.Points -= stolen;
        Points += stolen;
        return stolen;
    }
}
