using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class StackFillArea : MonoBehaviour
{
    public event Action<int,Team> OnAdded;
    public event Action<Team> OnCompleted;

    [ShowInInspector, ReadOnly] private Team[] _grid;
    [ShowInInspector, ReadOnly] private int _size;
    public bool Filled { get; private set; }
    public int Size => _size;

    public void SetSize(int size)
    {
        _size = size;
        _grid = new Team[Size];
    }

    public void AddStack(Team team)
    {
        for (int i = 0; i < Size; i++)
        {
            if (_grid[i] == team) continue;

            SetGridTeam(i, team);
            break;
        }
    }

    public int GetTeamFilledAmount(Team team)
    {
        var count = 0;
        for (int i = 0; i < Size; i++)
        {
            if (_grid[i] == team) count++;
        }
        return count;
    }

    private void SetGridTeam(int i,Team team)
    {
        _grid[i] = team;
        OnAdded?.Invoke(i, team);

        if (i < Size - 1) return;

        Filled = true;
        OnCompleted?.Invoke(team);
    }

}
