class_name BT_Repeat

extends BT_Node


## The number of times to repeat the child node
@export
var repeat_count: int = -1  # -1 for infinite

## The child node to repeat
@export
var child_node: BT_Node = null

func _init(child: BT_Node, count: int = -1):
    child_node = child
    repeat_count = count

func tick() -> int:
    if child_node == null:
        return NODE_STATE.FAIL

    var count = 0
    while repeat_count == -1 or count < repeat_count:
        var status = child_node.tick()
        if status == NODE_STATE.RUNNING:
            return NODE_STATE.RUNNING
        elif status == NODE_STATE.FAIL:
            return NODE_STATE.FAIL
        count += 1

    return NODE_STATE.SUCCESS