using System;
using System.Collections.Generic;
using UnityEngine;

public class AiBreakRockState : SubState<AiStackState>
{
    [SerializeField] private float _maxDistance;
    private Rock _target;

    public override void OnStateEnter(AiStackState baseState, CharacterController controller)
    {
        var rocks = RockManager.instance.GetAvailableObjects(controller.Team);
        if (rocks.Count <= 0)
        {
            baseState.SetState(baseState.getTargetState);
            return;
        }
        _target = GetClosestRock(controller, rocks);
    }

    public override void OnStateFixedUpdate(AiStackState baseState, CharacterController controller)
    {
        if (_target.Health <= 0)
        {
            baseState.SetState(baseState.getTargetState);
            return;
        }
        controller.Movement.MoveTo(_target.transform.position);
    }

    public Rock GetClosestRock(CharacterController controller, IReadOnlyList<Rock> rocks)
    {
        var closest = rocks[0];
        var closestDistance = Vector3.Distance(controller.transform.position, closest.transform.position);

        for (int i = 0; i < rocks.Count; i++)
        {
            var newDistance = Vector3.Distance(controller.transform.position, rocks[i].transform.position);
            if (newDistance > closestDistance) continue;

            closest = rocks[i];
            closestDistance = newDistance;
            if (closestDistance < _maxDistance) return closest;
        }
        return closest;
    }

}
