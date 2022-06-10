using DG.Tweening;

public class WinState : State
{
    protected override void OnStateEnter(CharacterController controller)
    {
        controller.DOKill();
        controller.Animation.SetWin();
        controller.Rigidbody.isKinematic = true;
    }
}
