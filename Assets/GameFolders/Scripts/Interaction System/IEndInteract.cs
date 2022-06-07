public interface IEndInteract
{
    bool IsInteractable { get; }
    void OnInteractEnd(IInteractor interactor);
}
