using UnityEngine;

public class CameraConfigSetter : MonoBehaviour
{
    [SerializeField] private CameraConfig gameplayConfig;
    [SerializeField] private CameraConfig driveConfig;
    [SerializeField] private CameraConfig finishConfig;

    private CharacterController _registeredCharacter;

    private void OnEnable()
    {
        GameManager.OnGameInitialized += OnInitialize;
        GameManager.OnGameEnd += OnEnd;
    }

    private void OnDisable()
    {
        GameManager.OnGameInitialized -= OnInitialize;
        GameManager.OnGameEnd -= OnEnd;
    }

    private void OnInitialize()
    {
        RegisterPlayer();
        CameraController.instance.ResetCamera();
    }

    private void OnEnd()
    {
        UnregisterPlayer();
        SetFinishCamera();
    }

    private void RegisterPlayer()
    {
        _registeredCharacter = CharacterManager.instance.player;
        _registeredCharacter.StackState.OnStateEntered += SetGamePlayCamera;
        _registeredCharacter.DriveState.OnStateEntered += SetDriveCamera;
    }

    private void UnregisterPlayer()
    {
        _registeredCharacter.StackState.OnStateEntered -= SetGamePlayCamera;
        _registeredCharacter.DriveState.OnStateEntered -= SetDriveCamera;

    }

    private void SetGamePlayCamera(CharacterController obj)
    {
        CameraController.instance.SetConfig(gameplayConfig);
    }
    private void SetDriveCamera(CharacterController obj)
    {
        CameraController.instance.SetConfig(driveConfig);
    }

    private void SetFinishCamera()
    {
        CameraController.instance.SetConfig(finishConfig);
    }


}
