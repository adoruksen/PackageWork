using System;

[Serializable]
public abstract class State
{
    public event Action<CharacterController> OnStateEntered;
    public event Action<CharacterController> OnStateExited;

    //public virtual void InitializeState(CharacterController controller) { }

    protected virtual void OnStateEnter(CharacterController controller) { }

    public virtual void OnStateFixedUpdate(CharacterController controller) { }

    protected virtual void OnStateExit(CharacterController controller) { }

    public void StateEnter(CharacterController controller)
    {
        OnStateEnter(controller);
        OnStateEntered?.Invoke(controller);
    }

    public void StateExit(CharacterController controller)
    {
        OnStateExit(controller);
        OnStateExited?.Invoke(controller);
    }
}
