extends BT_Node



## Calls the tick function for each child, returns success if all children were successful
func tick()->int:
	for child in self.bt_children:
		var child_tick: int = child.tick()
		if(child_tick == NODE_STATE.FAIL):
			return NODE_STATE.FAIL
		elif(child_tick == NODE_STATE.RUNNING):
			return NODE_STATE.RUNNING
	return NODE_STATE.SUCCESS
