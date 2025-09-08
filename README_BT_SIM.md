Setup for BT contest simulation

Files added:
- src/character/CharacterStats.cs : node to track points and special power
- src/game/Pickup.cs : pickup Area2D that adds to 'pickups' group and awards points/special
- src/game/GameController.cs : spawns pickups, checks collisions and win condition
- src/character/ai/actions/SeekPickup.cs : action to move toward nearest pickup
- src/character/ai/actions/Wander.cs : random wandering action
- src/character/ai/actions/Attack.cs : attack/steal action when special active
- src/character/ai/conditions/HasSpecial.cs : condition to check special power
- src/character/ai/conditions/NearestPickupExists.cs : condition to check pickups within radius
- src/character/ai/BasicAgent.tscn : example Basic agent scene using BT
- src/character/ai/AdaptiveAgent.tscn : example Adaptive agent scene (uses existing adaptation)

How to wire in Godot:
1. Open the project in Godot.
2. Create two instances of the agent scenes (BasicAgent.tscn and AdaptiveAgent.tscn). Name them AgentA and AgentB.
3. For each agent instance, set the `Opponent` NodePath to point to the other agent (e.g., AgentA set to ../AgentB, AgentB set to ../AgentA).
4. Ensure each agent has a `CharacterStats` child node named exactly `CharacterStats` (the scenes include placeholder).
5. Create a `GameController` node and assign `PickupScene` to a PackedScene created from a simple Area2D with `Pickup.cs` attached. Assign `SpecialPickupScene` similarly (set `IsSpecial=true` or use a separate PackedScene).
6. Set `AgentAPath` and `AgentBPath` on `GameController` to the two agents.
7. Start the scene. GameController will spawn pickups; agents will seek/wander and the special pickup will grant an attack ability.

Notes and assumptions:
- The agent BTs in scenes are basic examples; you can edit the BT structure in the editor to refine behaviors.
- Pickup scenes must be Area2D with collision shapes; the `Pickup.cs` script handles body_entered.
- Stealing amount and special duration can be tuned in `CharacterStats` and `GameController`.

Requirements coverage:
- Uses BT framework in src/BT: Done (actions and conditions inherit BT_ActionNode/BT_ConditionNode)
- Two NPCs, one adaptive: Done (BasicAgent and AdaptiveAgent scenes added)
- Pickup spawning and special pickup that allows stealing: Done (Pickup and GameController)
- First to 100 points wins: Done (GameController checks Points >= 100)

Next steps:
- Tweak scene art/collision shapes and attach scenes in Godot editor.
- Optionally implement more advanced adaptive logic to change attack priority when special granted.
