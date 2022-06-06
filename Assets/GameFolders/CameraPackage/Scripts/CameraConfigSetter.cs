using Sirenix.OdinInspector;
using UnityEngine;

public class CameraConfigSetter : MonoBehaviour
{
    [SerializeField] private CameraConfig gameplayConfig;
    [SerializeField] private CameraConfig finishConfig;

    public PlayerController _registeredCharacter;

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
        _registeredCharacter = PlayerController.instance;
        _registeredCharacter.MoveState.OnStateEntered += SetGamePlayCamera;
    }

    private void UnregisterPlayer()
    {
        _registeredCharacter.MoveState.OnStateEntered -= SetGamePlayCamera;
    }

    private void SetGamePlayCamera(PlayerController obj)
    {
        CameraController.instance.SetConfig(gameplayConfig);
    }

    private void SetFinishCamera()
    {
        CameraController.instance.SetConfig(finishConfig);
    }


}
