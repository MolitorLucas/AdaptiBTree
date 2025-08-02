extends Node
class_name BT_Action

var action: Node = null

func _init():
    pass

func _ready():
    # Ensure there is only one child node
    if get_child_count() > 0:
        action = get_child(0)

func tick() -> int:
    if action:
        # Call the child's tick method if it exists
        if action.has_method("tick"):
            return action.tick()
    return NODE_STATE.SUCCESS

func start():
    # Called when the action starts
    if action and action.has_method("start"):
        action.start()

func end():
    # Called when the action ends
    if action and action.has_method("end"):
        action.end()
