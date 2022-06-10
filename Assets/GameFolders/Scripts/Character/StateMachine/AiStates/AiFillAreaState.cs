using System;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

[Serializable]
public class AiFillAreaState : SubState<AiStackState>
{
    private StackFillArea _fillArea;

    public override void OnStateEnter(AiStackState baseState, CharacterController controller)
    {
        if (_fillArea==null || _fillArea.Filled)
        {
            FindFillArea(controller);
        }
    }

    public override void OnStateFixedUpdate(AiStackState baseState, CharacterController controller)
    {
        if (controller.StackController.Stack <= 0 || _fillArea.Filled)
        {
            controller.SetState(controller.StackState);
            return;
        }
        controller.Movement.MoveTo(_fillArea.transform.position);
    }

    public void FindFillArea(CharacterController controller)
    {
        var fillAreas = Object.FindObjectsOfType<StackFillArea>();
        _fillArea = fillAreas[Random.Range(0, fillAreas.Length)];
        while (Vector3.Distance(_fillArea.transform.position,controller.transform.position)>60f)
        {
            _fillArea = fillAreas[Random.Range(0, fillAreas.Length)];
        }
    }
}
