using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class VehicleController : MonoBehaviour,IInteractor,IHaveTeam
{
    public event Action<Team> OnTeamChanged;

    public Team Team => _team;
    [SerializeField, DisableInPlayMode] private Team _team;

    public AnimatorOverrideController characterAnimator;
    public VehicleSeat Seat { get; private set; }
    public VehicleMovement Movement { get; private set; }

    private void Awake()
    {
        Movement = GetComponent<VehicleMovement>();
        Seat = GetComponentInChildren<VehicleSeat>();
    }

    private void Start()
    {
        if (Team != null) OnTeamChanged?.Invoke(Team);
    }

    public void AssignTeam(Team team)
    {
        if (team == Team) return;
        _team = team;
        OnTeamChanged?.Invoke(Team);
    }
}
