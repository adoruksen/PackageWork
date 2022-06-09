
public class DriveState : State
{
    protected override void OnStateEnter(CharacterController controller)
    {
        controller.vehicle.AssignTeam(controller.Team);
        controller.Rigidbody.isKinematic = true;
        controller.Rigidbody.detectCollisions = false;
    }

    protected override void OnStateExit(CharacterController controller)
    {
        controller.Rigidbody.isKinematic = false;
        controller.Rigidbody.detectCollisions = true;
    }
}
