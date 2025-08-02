extends BT_Node


## Calls the tick function for the child, returns the opposite of the child's state
func tick()->int:
    var child_tick: int = self.bt_children[0].child.tick()
    if(child_tick == NODE_STATE.FAIL):
        return NODE_STATE.SUCCESS
    elif(child_tick == NODE_STATE.SUCCESS):
        return NODE_STATE.FAIL
    return NODE_STATE.RUNNING
