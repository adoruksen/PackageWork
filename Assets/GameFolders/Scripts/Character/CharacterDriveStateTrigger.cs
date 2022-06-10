using UnityEngine;

public class CharacterDriveStateTrigger : MonoBehaviour
{
    private VehicleController _vehicle;
    private StackFillArea _fillArea;
    private GameAreaManager _stackArea;

    private void Awake()
    {
        _fillArea = GetComponent<StackFillArea>();
        _stackArea = GetComponentInParent<GameAreaManager>();
    }

    private void OnEnable()
    {
        _fillArea.OnCompleted += UpdateCharacterState;
    }

    private void OnDisable()
    {
        _fillArea.OnCompleted -= UpdateCharacterState;
    }

    public void SetVehicle(VehicleController vehicle)
    {
        _vehicle = vehicle;
    }

    private void UpdateCharacterState(Team team)
    {
        var character = CharacterManager.instance.GetCharacterByTeam(team);
        character.SetState(character.EnterVehicleState);
        character.vehicle = _vehicle;
        _stackArea.OnCharacterExited(character);
    }
}
