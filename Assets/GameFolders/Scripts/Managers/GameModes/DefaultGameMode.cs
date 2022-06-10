using System;
using UnityEngine;
using DG.Tweening;

[CreateAssetMenu(menuName ="Game/GameMode/DefaultGameMode",fileName ="DefaultGameMode",order =-399)]
public class DefaultGameMode : GameMode
{
    public LevelConfig[] levels;
    [SerializeField] private CameraConfig _introConfig;

    [SerializeField] private bool _playCameraAnimation;
    [SerializeField] private float _cameraAnimationSpeed;
    [SerializeField] private AnimationCurve _cameraAnimationCurve;

    public override void InitializeGameMode()
    {
        var config = levels[GameManager.instance.GetSavedLevel() % levels.Length];
        LevelManager.instance.SpawnLevel(config.Parts);
        CharacterManager.instance.SpawnCharacters(config.Teams);
        foreach (var character in CharacterManager.instance.GetCharacters())
        {
            var startArea = LevelManager.instance.level.gameAreas[0];
            character.Movement.Bounds = startArea.PlayArea;
            startArea.OnCharacterEntered(character);
            character.SetState(character.IdleState);
        }
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
        var sequence = DOTween.Sequence();
        var gameAreas = LevelManager.instance.level.gameAreas;
        var target = new GameObject("CameraAnimationTarget").AddComponent<CameraFollowTarget>();
        target.transform.SetParent(GameManager.instance.defaultParent);
        CameraController.instance.SetConfig(_introConfig);
        CameraController.instance.SetTarget(target);
        var targetPosition = gameAreas[gameAreas.Length - 1].GetNextAreaPosition();
        var duration = targetPosition.z / _cameraAnimationSpeed;
        sequence.Append(target.transform.DOMoveZ(targetPosition.z, duration).SetEase(_cameraAnimationCurve));
        sequence.Append(target.transform.DOMoveZ(-0f, 1f).SetEase(Ease.InOutSine));
        sequence.OnComplete(StartGame);
        void StartGame()
        {
            CameraController.instance.SetTarget(CharacterManager.instance.player.GetComponent<CameraFollowTarget>());
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
        LevelManager.instance.DestroyLevel();
        CharacterManager.instance.DestroyCharacter();
        BrickManager.instance.RemoveAllObjects();
        RockManager.instance.RemoveAllObjects();
    }
}
