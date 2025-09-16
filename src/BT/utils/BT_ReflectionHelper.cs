using Godot;
using System;

public static class BT_ReflectionHelper
{
    public static void InsertSubTreeBelow(BT_Node root, BT_Node newSubtree)
    {
        root.AddChild(newSubtree);   
    }

    public static BT_Node InsertSubTreeAbove(BT_Node child, BT_Node newSubTree)
    {
        BT_Tree root = child.GetParent<BT_Tree>();
        root.RemoveChild(child);
        root.AddChild(newSubTree);
        newSubTree.AddChild(child);
        return newSubTree;
    }

    public static void RemoveSubtree(BT_Tree root, BT_Node subtreeToRemove)
    {
        if (root.HasNode(subtreeToRemove.GetPath()))
        {
            root.RemoveChild(subtreeToRemove);
            subtreeToRemove.QueueFree();
        }
    }
    public static void RemoveSubtree(BT_Node root, BT_Node subtreeToRemove)
    {
        if (root.HasNode(subtreeToRemove.GetPath()))
        {
            root.RemoveChild(subtreeToRemove);
            subtreeToRemove.QueueFree();
        }
    }
}
