using System;
using DG.Tweening;

[Serializable]
public class ExitVehicleState : State
{
    private bool _jumpingOut;

    protected override void OnStateEnter(CharacterController controller)
    {
        _jumpingOut = false;
    }

    public override void OnStateFixedUpdate(CharacterController controller)
    {
        if (_jumpingOut) return;
        controller.vehicle.Movement.Steer(0);

        if (controller.vehicle.Movement.CurrentSpeed > .1f) return;

        _jumpingOut = true;
        PlayExitSequence(controller);
    }

    private static void PlayExitSequence(CharacterController controller)
    {
        controller.vehicle.Seat.RemoveCharacter();
        controller.ExitVehicle().OnComplete(controller.ExitState);
    }
}
