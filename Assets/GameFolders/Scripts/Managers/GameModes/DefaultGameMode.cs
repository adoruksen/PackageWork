using System;
using UnityEngine;
using DG.Tweening;

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
        IntroUiController.Instance.ShowInstant();
    }

    public override void StartGameMode(Action levelStart)
    {
        IntroUiController.Instance.Hide();
        if (! _playCameraAnimation)
        {
            StartGame();
            return;
        }
        
        StartGame();
        void StartGame()
        {
            CameraController.instance.SetTarget(PlayerMovement.instance.GetComponent<CameraFollowTarget>());
            PlayerController.instance.SetState(PlayerController.instance.StackState);
            levelStart.Invoke();
        }
    }

    public override void CompleteGameMode()
    {
        GameManager.instance.SaveLevel(GameManager.instance.GetSavedLevel() + 1);
        DOVirtual.DelayedCall(1f,WinUiController.Instance.Show,false);
    }


    public override void FailGameMode()
    {
        DOVirtual.DelayedCall(2f,FailUiController.Instance.Show,false);
    }

    public override void DeinitializeGameMode()
    {
        //LevelManager.instance.DestroyLevel();
    }
}
