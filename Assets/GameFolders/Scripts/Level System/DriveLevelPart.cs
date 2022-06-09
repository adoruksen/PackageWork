using System;
using UnityEngine;
using Object = UnityEngine.Object;

[Serializable]
public class DriveLevelPart : LevelPart
{
    [SerializeField] private DriveAreaManager _driveArea;
    public override GameAreaManager SetupPart(Transform parent, GameAreaManager previousArea = null)
    {
        var manager = Object.Instantiate(_driveArea, parent);
        if (previousArea == null) return manager;

        var position = previousArea.GetNextAreaPosition();
        manager.MoveArea(position);
        return manager;
    }

    
}
