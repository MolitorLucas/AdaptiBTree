using Godot;
using System;
using Godot.Collections;

namespace BT
{

    [GlobalClass]
    public partial class Blackboard : Godot.Resource
    {
        [Export]
        public Dictionary<string, Variant> Data = [];

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
            GD.PrintErr($"Key '{key}' not found in the blackboard.");
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
}
