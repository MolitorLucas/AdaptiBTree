using BT.Constants;
using Godot;
using System;

namespace BT.Node {
    public partial class Root : Node
    {
        
        public 
        public override NodeState Tick()
        {
            foreach (var child in GetChildren())
            {
                if (child is Node node)
                {
                    var state = node.Tick();
                }
            }
            return Tick();
        }
    }
}