using UnityEngine;

public abstract class VehicleMovement : MonoBehaviour
{
    public Rigidbody Rigidbody { get; private set; }
    public VehicleData data;
    protected VehicleStats _stats;

    public float CurrentSpeed { get;protected set; }
    public Vector3 CurrentVelocity { get; protected set; }
    public float CurrentRotation { get; protected set; }

    private void Awake()
    {
        Rigidbody = GetComponent<Rigidbody>();
        SetStatsByGroundType(GroundType.Asphalt);
    }

    public void SetStatsByGroundType(GroundType type)
    {
        switch (type)
        {
            case GroundType.Asphalt:
                _stats = data.asphaltStats;
                break;
            case GroundType.Dirt:
                _stats = data.dirtStats;
                break;
            case GroundType.Oil:
                _stats = data.oilStats;
                break;
        }
    }

    public abstract void Accelerate();
    public abstract void Steer(float direction);
    public abstract void OnCollision(Collision other);

}
