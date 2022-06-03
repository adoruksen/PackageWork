using UnityEngine;
using System;

public abstract class GameMode : ScriptableObject
{
    public abstract void InitializeGameMode();
    public abstract void StartGameMode(Action levelStart);
    public abstract void FailGameMode();
    public abstract void CompleteGameMode();
    public abstract void DeinitializeGameMode();
    
}
