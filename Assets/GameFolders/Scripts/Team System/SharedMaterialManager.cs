using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class SharedMaterialManager : MonoBehaviour
{
    public static SharedMaterialManager instance;

    [SerializeField] private Material _characterMaterial;
    [SerializeField] private Material _vehicleMaterial;
    [SerializeField] private Material _brickMaterial;
    [SerializeField] private Material _rockMaterial;

    [ShowInInspector,ReadOnly] private Dictionary<Team,TeamMaterialGroup> _teamMaterials = new Dictionary<Team, TeamMaterialGroup> ();

    private void Awake()
    {
        instance = this;
    }

    public Material GetTeamMaterial(Team team,TeamMaterialGroup.Type type)
    {
        if (team==null)
        {
            return GetDefaultMaterialByType(type);
        }
        var teamRegistered = _teamMaterials.TryGetValue(team, out var group);
        if (!teamRegistered) group = RegisterTeam(team);

        return group.GetMaterialType(type);
    }

    private TeamMaterialGroup RegisterTeam(Team team)
    {
        var group = new TeamMaterialGroup(team, _characterMaterial, _vehicleMaterial, _brickMaterial, _rockMaterial);
        _teamMaterials.Add(team, group);
        return group;
    }

    private Material GetDefaultMaterialByType(TeamMaterialGroup.Type type)
    {
        return type switch
        {
            TeamMaterialGroup.Type.Character => _characterMaterial,
            TeamMaterialGroup.Type.Vehicle => _vehicleMaterial,
            TeamMaterialGroup.Type.Brick => _brickMaterial,
            TeamMaterialGroup.Type.Rock => _rockMaterial,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
