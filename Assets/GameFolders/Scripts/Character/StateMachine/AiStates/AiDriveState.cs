using System;
using UnityEngine;

[Serializable]
public class AiDriveState : DriveState
{
    [SerializeField] private float _noiseWeight;
    [SerializeField] private float _noiseSpeed;

    [SerializeField] private float _angle;
    [SerializeField] private float _rayCount;
    [SerializeField] private float _distance;
    [SerializeField] private float _obstacleWeight;
    [SerializeField] private LayerMask _layerMask;

    public override void OnStateFixedUpdate(CharacterController controller)
    {
        controller.vehicle.Movement.Accelerate();
        var direction = GetDirection(controller) * _obstacleWeight;
        direction += (Mathf.PerlinNoise(Time.fixedTime * _noiseSpeed, 0f) * 2 - 1) * _noiseWeight;
        direction = Mathf.Clamp(direction, -1f, 1f);
        controller.vehicle.Movement.Steer(direction);
    }

    private float GetDirection(CharacterController controller)
    {
        var origin = controller.Rigidbody.position;
        var value = 0f;
        var delta = 2 / (_rayCount - 1f);
        for (var i = -1f; i <= 1f; i += delta)
        {
            var angle = i * _angle * Mathf.Deg2Rad;
            var direction = new Vector3(Mathf.Sin(angle), 0f, Mathf.Cos(angle));
            var weight = GetWeight(origin, direction);
            value += i * weight * delta;
        }
        return value;
    }

    private float GetWeight(Vector3 origin,Vector3 direction)
    {
        var forwardRay = Physics.Raycast(origin, direction, out var hit, _distance, _layerMask);
#if UNITY_EDITOR
        if (forwardRay) Debug.DrawRay(origin, direction * hit.distance, Color.red);
        else Debug.DrawRay(origin, direction * _distance, Color.green);
#endif
        if (forwardRay) return 1f + Mathf.Clamp(Vector3.Dot(direction, hit.normal) * (_distance / hit.distance), -1, 0);
        return 1f;
    }
}
