using UnityEngine;

public class StackState : State
{
    protected override void OnStateEnter(CharacterController controller)
    {
        controller.Movement.UseBounds = true;
        controller.Bumper.isActive = true; 

    }

    protected override void OnStateExit(CharacterController controller)
    {
        controller.Movement.UseBounds = false;
        controller.Bumper.isActive = false;
    }
}
