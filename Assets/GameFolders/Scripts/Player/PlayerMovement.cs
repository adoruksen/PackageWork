using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public static PlayerMovement instance;
    private Rigidbody _rigidbody;
    public Transform Target;

    public float MoveSpeed;
    public bool IsActive;

    public bool UseBounds;
    public Bounds Bounds;

    private void Awake()
    {
        instance = this;
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 direction)
    {
        if (!IsActive) return;

        var movement = direction * MoveSpeed;
        _rigidbody.velocity = movement;
        if (UseBounds) _rigidbody.position = Bounds.ClosestPoint(_rigidbody.position);
    }

    public void MoveTo(Vector3 target)
    {
        if (!IsActive) return;

        var offset = target - _rigidbody.position;
        var direction = offset.sqrMagnitude > 1 ? offset.normalized : offset;
        direction.y = 0f;

        Move(direction);
        Look(direction);
    }

    public void MoveToTarget()
    {
        if (!IsActive) return;

        var position = Target.position;
        position.y = _rigidbody.position.y;
        MoveTo(position);
    }

    public void Look(Vector3 direction)
    {
        if (!IsActive) return;

        var rotation = Quaternion.Lerp(_rigidbody.rotation, Quaternion.LookRotation(direction), .2f);
        _rigidbody.MoveRotation(rotation);
    }
}
