using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class FinishLevelPart : LevelPart
{
    [SerializeField] private FinishAreaManager _finishArea;
    public override GameAreaManager SetupPart(Transform parent, GameAreaManager previousArea = null)
    {
        var manager = Object.Instantiate(_finishArea, parent);
        if (previousArea!=null)
        {
            var position = previousArea.GetNextAreaPosition();
            manager.MoveArea(position);
        }
        return manager;
    }
}
