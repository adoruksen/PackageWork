using System;
using UnityEngine;

[CreateAssetMenu(menuName ="Game/GameMode/DefaultGameMode",fileName ="DefaultGameMode",order =-399)]
public class DefaultGameMode : GameMode
{
    [SerializeField] private CameraConfig _introConfig;

    [SerializeField] private bool _playCameraAnimation;
    [SerializeField] private float _cameraAnimationSpeed;
    [SerializeField] private AnimationCurve _cameraAnimationCurve;

    public override void InitializeGameMode()
    {
        //level oluþtur karakterin stateini ayarla.
    }

    public override void StartGameMode(Action levelStart)
    {
        Debug.Log("default game mode StartGameMode");
        if (! _playCameraAnimation)
        {
            StartGame();
            return;
        }
        void StartGame()
        {
            CameraController.instance.SetTarget(PlayerMovement.instance.GetComponent<CameraFollowTarget>());
            levelStart.Invoke();
        }
    }

    public override void CompleteGameMode()
    {
        GameManager.instance.SaveLevel(GameManager.instance.GetSavedLevel() + 1);
        //DoVirtual.DelayedCall(1f,WinUiController.instance.Show,false);
    }


    public override void FailGameMode()
    {
        //DOVirtual.DelayedCall(2f,FailUiController.instance.Show,false);
    }

    public override void DeinitializeGameMode()
    {
        //LevelManager.instance.DestroyLevel();
    }
}
