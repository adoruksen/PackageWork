using UnityEngine;

public class StackState : State
{
    protected override void OnStateEnter(PlayerController controller)
    {
        //controller.Movement.UseBounds = true;
        //controller.Bumper.IsActive = true;
    }

    protected override void OnStateExit(PlayerController controller)
    {
        //controller.Movement.UseBounds = false;
        //controller.Bumper.IsActive = false;
    }
}
