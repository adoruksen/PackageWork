using System;
using UnityEngine;
using Object = UnityEngine.Object;

public class StackLevelPart : LevelPart
{
    [SerializeField] private StackAreaManager _manager;
    public VehicleData[] vehicles;

    public override GameAreaManager SetupPart(Transform parent, GameAreaManager previousArea = null)
    {
        var manager = Object.Instantiate(_manager, parent);
        if (previousArea!=null)
        {
            var position = previousArea.GetNextAreaPosition();
            manager.MoveArea(position);
        }

        manager.InitializeStackArea(vehicles);
        return manager;
    }
}
