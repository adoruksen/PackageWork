using Transform = UnityEngine.Transform;
public class FinishAreaManager : GameAreaManager
{
    public Transform[] podiumPlacers;
    public CameraFollowTarget cameraTarget;

    private bool _disabled;
    private void OnEnable()
    {
        GameManager.OnGameEnd += FinishSequence;
    }

    private void OnDisable()
    {
        GameManager.OnGameEnd -= FinishSequence;
    }

    private void FinishSequence()
    {
        CameraController.instance.followTarget = cameraTarget;
        SetStates();
        FillPodium();
    }

    public override void OnCharacterEntered(CharacterController character)
    {
        if (_disabled) return;

        _disabled = true;
        character.Movement.Target = podiumPlacers[0];
        character.SetState(character.FinishState);
    }

    public void FillPodium()
    {
        var rankOrderedCharacters = FinishManager.instance.RankedList;

        for (int i = 0; i < podiumPlacers.Length; i++)
        {
            rankOrderedCharacters[i].transform.position = podiumPlacers[i].position;
            rankOrderedCharacters[i].transform.rotation = podiumPlacers[i].rotation;
        }
    }

    private void SetStates()
    {
        var rankOrderedCharacters = FinishManager.instance.RankedList;
        for (int i = 0; i < rankOrderedCharacters.Count; i++)
        {
            var character = rankOrderedCharacters[i];
            if (character.vehicle != null) character.vehicle.Seat.RemoveCharacter();

            var state = i == 0 ? character.WinState : character.FailState;
            character.SetState(state);
        }
    }
}
