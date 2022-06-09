using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/Vehicle/VehicleData")]
public class VehicleData : ScriptableObject
{
    public VehicleController Vehicle => _vehicle;
    [SerializeField] private VehicleController _vehicle;

    public int Cost => _cost;
    [SerializeField] private int _cost;

    public VehicleStats asphaltStats;
    public VehicleStats dirtStats;
    public VehicleStats oilStats;
}

[Serializable]
public class VehicleStats
{
    [SerializeField] private float _maxSpeed;
    [SerializeField] private float _acceleration;
    [SerializeField] private float _maxRotation;
    [SerializeField] private float _handling;

    public float MaxSpeed => _maxSpeed;
    public float Acceleration => _acceleration;
    public float MaxRotation => _maxRotation;
    public float Handling => _handling;
}
