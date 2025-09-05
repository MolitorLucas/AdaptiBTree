using Godot;
using System;
using Godot.Collections;

[GlobalClass]
public partial class Blackboard : Resource
{
    private Dictionary<string, Variant> Data { get; set; } = [];


    public Variant this[string key]
    {
        get => GetValue(key);
        set => SetValue(key, value);
    }
    
    public void SetValue(string key, Variant value)
    {
        if (Data.ContainsKey(key))
        {
            Data[key] = value;
        }
        else
        {
            Data.Add(key, value);
        }
    }

    public Variant GetValue(string key)
    {
        if (Data.TryGetValue(key, out var value))
        {
            return value;
        }
        return "";
    }

    public bool HasKey(string key)
    {
        return Data.ContainsKey(key);
    }

    public void RemoveKey(string key)
    {
        Data.Remove(key);
    }

}

