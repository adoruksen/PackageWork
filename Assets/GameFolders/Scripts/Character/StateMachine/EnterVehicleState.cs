using System;
using DG.Tweening;
using UnityEngine;

[Serializable]
public class EnterVehicleState : State
{
    [SerializeField] private float _jumpDistance;
    private bool _sequenceStarted;

    protected override void OnStateEnter(CharacterController controller)
    {
        _sequenceStarted = false;
    }

    public override void OnStateFixedUpdate(CharacterController controller)
    {
        if (_sequenceStarted) return;
        var offset = controller.vehicle.transform.position - controller.Rigidbody.position;
        if (offset.magnitude <= _jumpDistance)
        {
            PlayEnterSequence(controller);
            _sequenceStarted = true;
            return;
        }

        controller.Movement.Move(offset.normalized);
        controller.Movement.Look(offset.normalized);
    }

    private void PlayEnterSequence(CharacterController controller)
    {
        if (controller.vehicle.characterAnimator != null) controller.Animation.SetAnimator(controller.vehicle.characterAnimator);
        controller.Animation.EnterVehicle();
        controller.Rigidbody.DOJump(controller.vehicle.Seat.Position, 2f, 1, .75f).OnComplete(() =>
        {
            controller.SetState(controller.DriveState);
            controller.vehicle.Seat.AddCharacter(controller);
        });
    }
}
