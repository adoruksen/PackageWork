using UnityEngine;

public abstract class VehicleAnimationController : MonoBehaviour
{
    protected Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }
}
