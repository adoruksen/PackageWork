using UnityEngine;
using DG.Tweening;

public class CharacterBumpController : MonoBehaviour
{
    private CharacterController _controller;
    public bool isActive;
    [SerializeField] private float _standDelay;
    [SerializeField] private float _verticalForce;
    [SerializeField] private float _horizontalForce;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
    }

    public void OnLostBumpTo(CharacterBumpController other)
    {
        isActive = false;
        _controller.Movement.IsActive = false;
        FlingCharacter(other);
        _controller.Animation.TriggerFall();
        _controller.StackController.LoseAllStack();
        _controller.Interactor.canInteract = false;
        DOVirtual.DelayedCall(_standDelay, ResetBump, false);
    }

    private void FlingCharacter(Component other)
    {
        var direction = (transform.position - other.transform.position).normalized;
        direction *= _horizontalForce;
        direction.y = _verticalForce;
        _controller.Rigidbody.velocity = direction;
    }

    private void ResetBump()
    {
        _controller.Movement.IsActive = true;
        _controller.Interactor.canInteract = true;
        isActive = true;
    }

    public void HandleBumped(CharacterBumpController other)
    {
        if (!this.isActive || !other.isActive) return;

        var thisStack = _controller.StackController.Stack;
        var otherStack = other._controller.StackController.Stack;

        if (thisStack == otherStack) return;
        if(thisStack <= 0 || otherStack <= 0) return;
        if (thisStack> otherStack)
        {
            other.OnLostBumpTo(this);
        }
        else
        {
            this.OnLostBumpTo(other);
        }
    }
}
