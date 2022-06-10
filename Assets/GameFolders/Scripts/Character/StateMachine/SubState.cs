using System;

public class SubState<T> where T:State 
{
    public virtual void OnStateEnter(T baseState,CharacterController controller) { }
    public virtual void OnStateFixedUpdate(T baseState,CharacterController controller) { }
    public virtual void OnStateExit(T baseState,CharacterController controller) { }
}
