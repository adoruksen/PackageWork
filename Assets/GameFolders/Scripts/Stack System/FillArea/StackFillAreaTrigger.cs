using UnityEngine;

public class StackFillAreaTrigger : MonoBehaviour ,IBeginInteract,IStayInteract,IEndInteract
{
    private StackFillArea _fillArea;
    public bool IsInteractable { get; private set; }
    private float _timer;

    [SerializeField] private float _cooldown;

    private void Awake()
    {
        _fillArea = GetComponentInParent<StackFillArea>();
        IsInteractable = true;
    }

    public void OnInteractBegin(IInteractor interactor)
    {
        var controller = (CharacterController)interactor;
        AddStack(controller);
    }

    public void OnInteractStay(IInteractor interactor)
    {
        _timer += Time.fixedDeltaTime;
        if (_timer <= _cooldown) return;

        var controller = (CharacterController)interactor;
        AddStack(controller);
    }

    public void OnInteractEnd(IInteractor interactor)
    {
        var controller = (CharacterController)interactor;
        controller.Bumper.isActive = true;
    }

    private void AddStack(CharacterController controller)
    {
        var stackController = controller.GetComponent<StackController>();
        if (stackController.Stack <= 0) return;
        _timer = 0f;
        stackController.UseStack();

        _fillArea.AddStack(controller.Team);
        if (_fillArea.Filled) IsInteractable = false;
    }
}
