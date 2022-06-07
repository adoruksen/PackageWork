using UnityEngine;
using System;
using Sirenix.OdinInspector;

public class Brick : MonoBehaviour ,IBeginInteract
{
    public event Action OnCollected;

    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] private Collider _collider;
    [SerializeField] private float _verticalForce;
    [SerializeField] private float _horizontalForce;

    [ShowInInspector,ReadOnly] public bool IsInteractable { get;private set; } = true;

    public void OnInteractBegin(IInteractor interactor)
    {
        var controller = (PlayerController)interactor;
        Collect(controller);
    }

    private void Collect(PlayerController controller)
    {
        OnCollected?.Invoke();
        controller.StackController.AddStack(this);
        SetInteractable(false);
    }

    public void SetInteractable(bool interactable)
    {
        _collider.enabled = interactable;
        _rigidbody.isKinematic = !interactable;
        IsInteractable = interactable;
        BrickManager.instance.SetObjectAvailable(this, interactable);
    }
}
