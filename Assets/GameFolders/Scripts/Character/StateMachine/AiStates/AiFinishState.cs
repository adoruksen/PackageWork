public class AiFinishState : FinishState
{
    protected override void OnStateEnter(CharacterController controller)
    {
        FinishManager.instance.FinishLevel();
    }
}
