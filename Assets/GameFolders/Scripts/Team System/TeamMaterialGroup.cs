using System;
using UnityEngine;

public class TeamMaterialGroup 
{
    public enum Type
    {
        Character,
        Vehicle,
        Brick,
        Rock
    }

    public Material characterMat;
    public Material vehicleMat;
    public Material brickMat;
    public Material rockMat;

    public TeamMaterialGroup(Team team, params Material[] materials)
    {
        characterMat = new Material(materials[0]) { color = team.CharacterColor };
        vehicleMat = new Material(materials[1]) { color = team.VehicleColor };
        brickMat = new Material(materials[2]) { color = team.BrickColor };
        rockMat = new Material(materials[3]) { color = team.RockColor };
    }

    public Material GetMaterialType(Type type)
    {
        return type switch
        {
            Type.Character => characterMat,
            Type.Vehicle => vehicleMat,
            Type.Brick => brickMat,
            Type.Rock => rockMat,
            _ => throw new ArgumentOutOfRangeException(nameof(type), type, null)
        };
    }
}
