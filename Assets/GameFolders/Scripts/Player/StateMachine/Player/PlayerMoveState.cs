using System;
using UnityEngine;

[Serializable]
public class PlayerMoveState : MoveState
{
    public override void OnStateFixedUpdate(PlayerController controller)
    {
        var offset = InputManager.Module.NormalizedOffset;
        var direction = new Vector3(offset.x, 0f, offset.y);
        controller.Movement.Move(direction);
        if (offset.sqrMagnitude > .001f) controller.Movement.Look(direction);
    }
}
