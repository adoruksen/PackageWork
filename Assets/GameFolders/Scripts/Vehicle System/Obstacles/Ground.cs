using UnityEngine;

public class Ground : MonoBehaviour ,IBeginInteract,IEndInteract
{
    public bool IsInteractable { get; private set; } = true;
    public GroundType Type;

    public void OnInteractBegin(IInteractor interactor)
    {
        var controller =(VehicleController)interactor;
        controller.Movement.SetStatsByGroundType(Type);
    }

    public void OnInteractEnd(IInteractor interactor)
    {
        var controller = (VehicleController)interactor;
        controller.Movement.SetStatsByGroundType(GroundType.Asphalt);
    }
}
