using BT.Constants;
using Godot;
using System;

namespace BT.Node
{
    [GlobalClass]
    public abstract partial class Node : Godot.Node
    {
        public abstract NodeState Tick();
        
    }
}
