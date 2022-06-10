using System;

[Serializable]
public class PlayerDriveState : DriveState
{
    public override void OnStateFixedUpdate(CharacterController controller)
    {
        var offset = InputManager.Module.NormalizedOffset;
        var direction = offset.x;
        controller.vehicle.Movement.Accelerate();
        controller.vehicle.Movement.Steer(direction);
    }
}
