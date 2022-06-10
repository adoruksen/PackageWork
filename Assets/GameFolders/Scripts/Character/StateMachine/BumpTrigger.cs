using UnityEngine;

public class BumpTrigger : MonoBehaviour ,IBeginInteract
{
    private CharacterBumpController _bumpController;
    [SerializeField] private Collider _thisInteractor;
    public bool IsInteractable { get; } = true;

    private void Awake()
    {
        _bumpController = GetComponentInParent<CharacterBumpController>();
        Physics.IgnoreCollision(GetComponent<Collider>(), _thisInteractor);
    }

    public void OnInteractBegin(IInteractor interactor)
    {
        var controller = (CharacterController)interactor;
        controller.GetComponent<CharacterBumpController>().HandleBumped(_bumpController);
    }
}
