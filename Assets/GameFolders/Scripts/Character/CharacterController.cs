using System;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class CharacterController : MonoBehaviour ,IInteractor ,IHaveTeam
{
    public static CharacterController instance;
    [SerializeReference, BoxGroup("Idle", false), HorizontalGroup("Idle/Group")] public State IdleState;
    [SerializeReference, BoxGroup("Stack", false), HorizontalGroup("Stack/Group")] public StackState StackState;
    [SerializeReference, BoxGroup("Drive", false), HorizontalGroup("Drive/Group")] public DriveState DriveState;
    [SerializeReference, BoxGroup("Enter", false), HorizontalGroup("Enter/Group")] public EnterVehicleState EnterVehicleState;
    [SerializeReference, BoxGroup("Exit", false), HorizontalGroup("Exit/Group")] public ExitVehicleState ExitVehicleState;
    [SerializeReference, BoxGroup("Finish", false), HorizontalGroup("Finish/Group")] public FinishState FinishState;
    [SerializeReference, BoxGroup("Winn", false), HorizontalGroup("Winn/Group")] public State WinState;
    [SerializeReference, BoxGroup("Fail", false), HorizontalGroup("Fail/Group")] public State FailState;
    [ShowInInspector, ReadOnly, BoxGroup("States", false)] public State CurrentState { get; private set; }

    public event Action<Team> OnTeamChanged;
    public Team Team => _team;
    [SerializeField, DisableInPlayMode] private Team _team;

    public Rigidbody Rigidbody { get; private set; }
    public Interactor Interactor { get; private set; }  
    public CharacterMovement Movement { get; private set; }
    public CharacterBumpController Bumper { get; private set; }
    public CharacterAnimationController Animation { get; private set; }
    public StackController StackController { get; private set; }
    public VehicleController vehicle;


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
        Bumper = GetComponent<CharacterBumpController>();
        Movement = GetComponent<CharacterMovement>();
        StackController = GetComponent<StackController>();
        Animation = GetComponent<CharacterAnimationController>();
    }


    private void FixedUpdate()
    {
        CurrentState?.OnStateFixedUpdate(this);
    }

    public void AssignTeam(Team team)
    {
        if (team == Team) return;

        _team = team;
        OnTeamChanged?.Invoke(Team);
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

    public Tween ExitVehicle()
    {
        vehicle = null;
        Animation.ExitVehicle();
        var jumpPosition = Movement.Bounds.ClosestPoint(Rigidbody.position);
        return Rigidbody.DOJump(jumpPosition, 2f, 1, .75f);
    }
}
