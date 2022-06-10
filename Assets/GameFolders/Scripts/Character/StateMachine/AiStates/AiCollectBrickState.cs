using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AiCollectBrickState : SubState<AiStackState>
{
    [SerializeField] private float _maxDistance;
    private Brick _target;

    public override void OnStateEnter(AiStackState baseState, CharacterController controller)
    {
        var bricks = BrickManager.instance.GetAvailableObjects(null);
        if (bricks.Count >0)
        {
            _target = bricks[Random.Range(0, bricks.Count)];
            return;
        }

        bricks = BrickManager.instance.GetAvailableObjects(controller.Team);
        if (bricks.Count>0)
        {
            _target = GetClosestBrick(controller, bricks);
            if (_target) return;
        }
        baseState.SetState(baseState.getTargetState);
    }

    public override void OnStateFixedUpdate(AiStackState baseState, CharacterController controller)
    {
        if (!_target.IsInteractable)
        {
            baseState.SetState(baseState.getTargetState);
            return;
        }
        controller.Movement.MoveTo(_target.transform.position);
    }

    private Brick GetClosestBrick(CharacterController controller, IReadOnlyList<Brick> bricks)
    {
        Brick closest = null;
        var closestDistance = 60f;
        for (int i = 0; i < bricks.Count; i++)
        {
            var newDistance = Vector3.Distance(controller.transform.position, bricks[i].transform.position);
            if (newDistance > closestDistance) continue;

            closest=bricks[i];
            closestDistance = newDistance;
            if (closestDistance < _maxDistance) return closest;
        }
        return closest;
    }
}
