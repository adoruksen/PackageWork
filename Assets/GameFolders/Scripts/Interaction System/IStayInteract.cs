public interface IStayInteract
{
    bool IsInteractable { get; }
    void OnInteractStay(IInteractor interactor);
}
