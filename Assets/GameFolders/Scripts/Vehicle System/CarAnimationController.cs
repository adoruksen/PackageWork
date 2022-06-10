using UnityEngine;

public class CarAnimationController : VehicleAnimationController
{
    [SerializeField] private Transform[] _steerWheels;
    [SerializeField] private Transform[] _wheels;
    [SerializeField] private Transform _vehicleBody;
    [SerializeField] private Transform _vehicle;

    [SerializeField] private float _maxTilt;

    private void FixedUpdate()
    {
        var velocity = _rigidbody.velocity;
        var speed = velocity.magnitude;
        if (velocity.sqrMagnitude < .01f) return;

        var rotation = Quaternion.LookRotation(velocity.normalized);

        RotateBody(rotation);
        SteerWheels(rotation);
        RotateWheels(speed);
        TiltBody(velocity);
    }

    private void SteerWheels(Quaternion rotation)
    {
        foreach (var wheel in _steerWheels)
        {
            wheel.rotation = Quaternion.Lerp(wheel.rotation, rotation, 1f);
        }
    }

    private void RotateWheels(float speed)
    {
        foreach (var wheel in _wheels)
        {
            wheel.localRotation *= Quaternion.Euler(speed, 0f, 0f);
        }
    }

    private void TiltBody(Vector3 velocity)
    {
        var bodyRotation = Vector3.SignedAngle(_vehicleBody.forward, velocity, Vector3.up);
        bodyRotation = Mathf.Clamp(bodyRotation, -_maxTilt, _maxTilt);
        _vehicleBody.localRotation = Quaternion.Lerp(_vehicleBody.localRotation, Quaternion.Euler(0f, 0f, bodyRotation), .2f);
    }

    private void RotateBody(Quaternion rotation)
    {
        _vehicle.rotation = Quaternion.Lerp(_vehicle.rotation, rotation, .2f);
    }
}
