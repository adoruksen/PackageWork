using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeReference, BoxGroup("Idle", false), HorizontalGroup("Idle/Group")] public State IdleState;
    [SerializeReference, BoxGroup("Move", false), HorizontalGroup("Move/Group")] public MoveState MoveState;
    [SerializeReference, BoxGroup("Fnsh", false), HorizontalGroup("Fnsh/Group")] public FinishState FinishState;
    //[SerializeReference, BoxGroup("Winn", false), HorizontalGroup("Winn/Group")] public State WinState;
    //[SerializeReference, BoxGroup("Fail", false), HorizontalGroup("Fail/Group")] public State FailState;
    [ShowInInspector, ReadOnly, BoxGroup("States", false)] public State CurrentState { get; private set; }


    public Rigidbody Rigidbody { get; private set; }
    public PlayerMovement Movement { get; private set; }
    //public PlayerAnimationController Animation { get; private set; }


    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Movement = GetComponent<PlayerMovement>();
        //Animation = GetComponent<PlayerAnimationController>();
        SetState(IdleState);
    }


    private void FixedUpdate()
    {
        CurrentState?.OnStateFixedUpdate(this);
    }

    public void ExitState()
    {
        CurrentState?.StateExit(this);
    }

    public void SetState(State newState)
    {
        ExitState();
        CurrentState = newState;
        CurrentState.StateEnter(this);
    }
}
