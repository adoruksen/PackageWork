using System.Collections;
using System.Collections.Generic;
using System;

[Serializable]
public abstract class State
{
    public event Action<PlayerController> OnStateEntered;
    public event Action<PlayerController> OnStateExited;

    //public virtual void InitializeState(CharacterController controller) { }

    protected virtual void OnStateEnter(PlayerController controller) { }

    public virtual void OnStateFixedUpdate(PlayerController controller) { }

    protected virtual void OnStateExit(PlayerController controller) { }

    public void StateEnter(PlayerController controller)
    {
        OnStateEnter(controller);
        OnStateEntered?.Invoke(controller);
    }

    public void StateExit(PlayerController controller)
    {
        OnStateExit(controller);
        OnStateExited?.Invoke(controller);
    }
}
