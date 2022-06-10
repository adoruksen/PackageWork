using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class DoorAnimationController : MonoBehaviour
{
    private StackFillArea _fillArea;

    [SerializeField] private Animation[] _animations;
    [SerializeField] private Rigidbody _padLockRigidbody;
    [SerializeField] private float _flingHorizontalForce;
    [SerializeField] private float _flingVerticalForce;
    [SerializeField] private float _flingTorque;
    [SerializeField] private float _flingDelay;

    private void Awake()
    {
        _fillArea = GetComponentInParent<StackFillArea>();
    }
    private void OnEnable()
    {
        _fillArea.OnCompleted += OpenDoor;
    }

    private void OnDisable()
    {
        _fillArea.OnCompleted -= OpenDoor;
    }

    private void OpenDoor(Team obj)
    {
        foreach (var animation in _animations)
        {
            animation.Play();
        }
        DOVirtual.DelayedCall(_flingDelay, FlingPadlock, false);
    }

    private void FlingPadlock()
    {
        _padLockRigidbody.isKinematic = false;
        var force = Random.insideUnitCircle * _flingHorizontalForce;
        force.y = (Random.value + .5f) * _flingVerticalForce;
        _padLockRigidbody.AddForce(force);
        var torque = Random.onUnitSphere * _flingTorque;
        _padLockRigidbody.AddTorque(torque);
    }

#if UNITY_EDITOR
    [Button, DisableInEditorMode]
    private void OpenDoor() => OpenDoor(null);
#endif
}
