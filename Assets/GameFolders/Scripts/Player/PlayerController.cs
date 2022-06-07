using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class PlayerController : MonoBehaviour ,IInteractor
{
    public static PlayerController instance;
    [SerializeReference, BoxGroup("Idle", false), HorizontalGroup("Idle/Group")] public State IdleState;
    [SerializeReference, BoxGroup("Stack", false), HorizontalGroup("Stack/Group")] public StackState StackState;
    [SerializeReference, BoxGroup("Fnsh", false), HorizontalGroup("Fnsh/Group")] public FinishState FinishState;
    //[SerializeReference, BoxGroup("Winn", false), HorizontalGroup("Winn/Group")] public State WinState;
    //[SerializeReference, BoxGroup("Fail", false), HorizontalGroup("Fail/Group")] public State FailState;
    [ShowInInspector, ReadOnly, BoxGroup("States", false)] public State CurrentState { get; private set; }


    public Rigidbody Rigidbody { get; private set; }
    public Interactor Interactor { get; private set; }  
    public PlayerMovement Movement { get; private set; }
    public PlayerAnimationController Animation { get; private set; }
    public StackController StackController { get; private set; }


    private void Awake()
    {
        instance = this;
        ComponentSetter();
        SetState(IdleState);
    }

    private void ComponentSetter()
    {
        Rigidbody = GetComponent<Rigidbody>();
        Interactor = GetComponentInChildren<Interactor>();
        Movement = GetComponent<PlayerMovement>();
        StackController = GetComponent<StackController>();
        Animation = GetComponent<PlayerAnimationController>();
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
