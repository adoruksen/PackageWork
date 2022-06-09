using UnityEngine;

public class CarMovement : VehicleMovement
{
    private bool _isAccelerating;
    [SerializeField] private float _overspeedCorrection;
    [SerializeField] private float _oversteerCorrection;

    private void FixedUpdate()
    {
        if (!_isAccelerating)
        {
            CurrentSpeed -= _stats.MaxSpeed * Time.fixedDeltaTime;
            CurrentSpeed = Mathf.Max(CurrentSpeed, 0f);
        }
        else
        {
            var acceleration = _stats.Acceleration;
            CurrentSpeed += acceleration * Time.fixedDeltaTime;
            _isAccelerating = false;
        }

        var maxSpeed = _stats.MaxSpeed;
        CurrentSpeed = CurrentSpeed <= maxSpeed ? CurrentSpeed:Mathf.Lerp(CurrentSpeed, maxSpeed, _overspeedCorrection);

        var speed = Vector3.forward * CurrentSpeed;
        CurrentVelocity = Quaternion.Euler(0, CurrentRotation, 0) * speed;
        Rigidbody.velocity = CurrentVelocity;
    }
    public override void Accelerate()
    {
        _isAccelerating = true;
    }

    public override void OnCollision(Collision other)
    {
        var normal = other.contacts[0].normal;
        var rad = CurrentRotation * Mathf.Deg2Rad;
        var velocity = new Vector3(Mathf.Sin(rad), 0f, Mathf.Cos(rad)); //x => sin , y => cos
        var dot = -Vector3.Dot(normal, velocity);
        var tangent = (velocity + dot * 1.3f * normal).normalized;
        var angle = Vector3.SignedAngle(Vector3.forward, tangent, Vector3.up);
        CurrentSpeed *= 1 - dot;
        CurrentRotation = Mathf.Clamp(angle, -90f, 90f);
    }

    public override void Steer(float angle)
    {
        var difference = angle * _stats.MaxRotation - CurrentRotation;
        difference = ClampDifference(difference);
        CurrentRotation += difference;
        CurrentRotation = CurrentRotation <= _stats.MaxRotation ? CurrentRotation : Mathf.Lerp(CurrentRotation, _stats.MaxRotation, _oversteerCorrection);
    }

    private float ClampDifference(float difference)
    {
        var handling = _stats.Handling;
        if (difference > handling) difference = handling;
        else if(difference < -handling) difference = -handling;
        return difference;
    }

}
public enum GroundType
{
    Asphalt = 1,
    Dirt = 2,
    Oil = 3
}
