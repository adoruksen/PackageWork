using System;

[Serializable]
public class AiStackState : StackState
{
    public AiSelectTargetState getTargetState;
    public AiCollectBrickState collectState;
    public AiBreakRockState breakRockState;
    public AiFillAreaState fillAreaState;

    protected SubState<AiStackState> _currentSubState;
    private CharacterController _controller;

    protected override void OnStateEnter(CharacterController controller)
    {
        base.OnStateEnter(controller);

        _controller = controller;
        SetState(getTargetState);
    }

    public override void OnStateFixedUpdate(CharacterController controller)
    {
        base.OnStateFixedUpdate(controller);
        _currentSubState.OnStateFixedUpdate(this,controller);
    }

    protected override void OnStateExit(CharacterController controller)
    {
        base.OnStateExit(controller);
        _currentSubState.OnStateExit(this, controller);
    }

    public void SetState(SubState<AiStackState> subState)
    {
        _currentSubState?.OnStateExit(this, _controller);
        _currentSubState = subState;
        _currentSubState.OnStateEnter(this, _controller);

    }
}
