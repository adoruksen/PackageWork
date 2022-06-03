using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public CameraFollowTarget followTarget;
    public Vector3 offset;
    public float followSpeed;

    private Vector3 _targetPosition;
    private Quaternion _targetRotation;

    private Vector3 _defaultPosition;
    private Quaternion _defaultRotation;

    public bool isActive;
    public bool useFixedUpdate;

    private void Awake()
    {
        instance = this;
        _defaultPosition = transform.position;
        _defaultRotation = transform.rotation;
        _targetPosition = _defaultPosition;
        _targetRotation = _defaultRotation;
    }

    private void Update()
    {
        if (useFixedUpdate) return;

        DoCamera();
    }

    private void FixedUpdate()
    {
        if(!useFixedUpdate) return;

        DoCamera();
    }

    private void DoCamera()
    {
        if (followTarget != null) _targetPosition = followTarget.Position + offset;
        transform.position = Vector3.Lerp(transform.position, _targetPosition, followSpeed);
        transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, followSpeed);
    }

    public void SetTarget(CameraFollowTarget target)
    {
        followTarget = target;
    }

    public void SetConfig(CameraConfig config)
    {
        offset = config.offset;
        _targetRotation = Quaternion.AngleAxis(config.rotation, Vector3.right);
    }

    public void ResetCamera()
    {
        transform.position = _defaultPosition;
        transform.rotation = _defaultRotation;
        _targetPosition = _defaultPosition;
        _targetRotation = _defaultRotation;
    }
}
