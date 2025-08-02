## Abstract class that represents an Node of the Behavior Tree
## The other specific nodes inherits this class.
class_name BT_Node
extends Node


func _init(parent: BT_Node) -> void:
	self.bt_parent = parent

## Tick through the node, needs to be overwritten
func tick()->int:
	push_error("Method 'tick' not implemented")
	return -1;
