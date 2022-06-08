using UnityEngine;

[CreateAssetMenu(menuName ="Game/TeamSystem/Team",order = -399)]
public class Team : ScriptableObject
{
    public Color CharacterColor => _characterColor;
    [SerializeField] private Color _characterColor;

    public Color VehicleColor => _vehicleColor;
    [SerializeField] private Color _vehicleColor;
    public Color RockColor => _rockColor;
    [SerializeField] private Color _rockColor;
    public Color BrickColor => _brickColor;
    [SerializeField] private Color _brickColor;
}
