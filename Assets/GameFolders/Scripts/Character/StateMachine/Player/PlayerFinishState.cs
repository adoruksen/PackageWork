using System;
using DG.Tweening;
using UnityEngine;

public class PlayerFinishState : FinishState
{
    [SerializeField] private float _jumpDistance;
    private bool _jumped;

    protected override void OnStateEnter(CharacterController controller)
    {
        _jumped = false;
    }

    public override void OnStateFixedUpdate(CharacterController controller)
    {
        if (_jumped) return;

        var distance = Vector3.Distance(controller.Movement.Target.position, controller.Rigidbody.position);
        if (distance > _jumpDistance)
        {
            controller.Movement.MoveToTarget();
            return;
        }

        JumpToPodiumSequence(controller);
    }

    private void JumpToPodiumSequence(CharacterController controller)
    {
        _jumped = true;
        controller.Rigidbody.DOJump(controller.Movement.Target.position, 2f, 1, .5f)
                            .OnComplete(() => FinishManager.instance.FinishLevel());
    }
}