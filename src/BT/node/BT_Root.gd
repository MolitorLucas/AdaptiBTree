class_name BT_ROOT

extends BT_Node

var blackboard: Blackboard = Blackboard.new()

var active: bool = true
var last_status: 

func tick() -> int:
	if(!active):
		return NODE_STATE.SUCCESS
	for child in get_children():
		last_leaf = child
		child.tick()
	return tick()

