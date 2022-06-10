using UnityEngine;

public class VehicleCollisionDetector : MonoBehaviour
{

    private VehicleMovement _movement;

    private void Awake()
    {
        _movement= GetComponent<VehicleMovement>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _movement.OnCollision(other);
    }
}
