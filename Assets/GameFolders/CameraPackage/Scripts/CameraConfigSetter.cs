using Sirenix.OdinInspector;
using UnityEngine;

public class CameraConfigSetter : MonoBehaviour
{
    [SerializeField] private CameraConfig gameplayConfig;
    [SerializeField] private CameraConfig introConfig;

    [SerializeField] private CameraConfig finishConfig;
    public bool isGame;

    public GameObject _registeredCharacter;

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

    private void Update()
    {
        if (isGame)
        {
            SetGamePlayCamera(_registeredCharacter);
        }
    }
    private void OnInitialize()
    {
        SetIntroCamera();
        CameraController.instance.ResetCamera();
    }

    private void OnEnd()
    {
        SetFinishCamera();
    }


    private void SetGamePlayCamera(GameObject obj)
    {
        CameraController.instance.SetConfig(gameplayConfig);
    }

    private void SetIntroCamera()
    {
        CameraController.instance.SetConfig(introConfig);
    }

    private void SetFinishCamera()
    {
        CameraController.instance.SetConfig(finishConfig);
    }

}
