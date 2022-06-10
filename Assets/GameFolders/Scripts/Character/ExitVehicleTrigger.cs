using UnityEngine;

public class ExitVehicleTrigger : MonoBehaviour, IBeginInteract
{
    private GameAreaManager _newArea;
    public bool IsInteractable { get; }=true;

    private void Awake()
    {
        _newArea = GetComponentInParent<GameAreaManager>();
    }

    public void OnInteractBegin(IInteractor interactor)
    {
        var vehicle = (VehicleController)interactor;
        var character = CharacterManager.instance.GetCharacterByTeam(vehicle.Team);
        character.Movement.Bounds = _newArea.PlayArea;
        character.SetState(character.ExitVehicleState);
        character.ExitVehicleState.OnStateExited += EnterGameArea;
    }

    private void EnterGameArea(CharacterController character)
    {
        character.ExitVehicleState.OnStateExited -= EnterGameArea;
        _newArea.OnCharacterEntered(character);
    }

    
}
