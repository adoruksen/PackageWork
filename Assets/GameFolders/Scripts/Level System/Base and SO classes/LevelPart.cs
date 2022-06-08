using System;
using UnityEngine;

[Serializable]
public abstract class LevelPart
{
    public abstract GameAreaManager SetupPart(Transform parent, GameAreaManager previousArea = null);
}
