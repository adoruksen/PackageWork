using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : RuffGameManager
{
    public static event GameEvents OnGameInitialized;
    public static event GameEvents OnGameEnd;

    public static GameManager instance;

    [SerializeField] private GameMode _defaultGameMode;
    private GameMode _currentGameMode;

    public bool isPlaying { get; private set; }

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        InitializeGameMode(_defaultGameMode);
    }

    public void InitializeGameMode(GameMode gameMode)
    {
        if (_currentGameMode != null) _currentGameMode.DeinitializeGameMode();
        _currentGameMode = gameMode;
        _currentGameMode.InitializeGameMode();
        LevelInitialize();
    }

    public void StartGameMode()
    {
        _currentGameMode.StartGameMode(LevelStart);
        isPlaying = true;
    }

    public void CompleteGameMode()
    {
        LevelEnd();
        LevelComplete();
        isPlaying = false;
        _currentGameMode.CompleteGameMode();
    }

    public override void JumpToLevel(int targetLevel)
    {
        SaveLevel(targetLevel);
        InitializeGameMode(_currentGameMode);
    }

    public void FailGameMode()
    {
        isPlaying = false;
        _currentGameMode.FailGameMode();
        LevelEnd();
        LevelFail();
    }

    public override int GetLevel()
    {
        return GetSavedLevel();
    }

    public override string GetLevelString()
    {
        return GetLevel().ToString();
    }

    protected void LevelInitialize()
    {
        OnGameInitialized?.Invoke();
    }

    protected void LevelEnd()
    {
        OnGameEnd?.Invoke();
    }

}
