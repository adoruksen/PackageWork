using System;
using UnityEngine;

public class FillObject : MonoBehaviour, IHaveTeam
{
    public Team Team {get; private set;}

    public event Action<Team> OnTeamChanged;

    public void AssignTeam(Team team)
    {
        if (team == Team) return;
        
        Team = team;
        OnTeamChanged?.Invoke(Team);
    }
}
