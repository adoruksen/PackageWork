using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public static event Action<LevelController> OnLevelSpawned;

    [ReadOnly] public LevelController level;

    private void Awake()
    {
        instance = this;   
    }

    [Button]
    public void SpawnLevel(LevelPart[] parts)
    {
        level = new GameObject("Level").AddComponent<LevelController>();
        var gameAreas = new List<GameAreaManager>();

        var prevArea = parts[0].SetupPart(level.transform);
        gameAreas.Add(prevArea);
        for (var i = 1; i < parts.Length; i++)
        {
            var area = parts[i].SetupPart(level.transform, prevArea);
            gameAreas.Add(area);
            prevArea = area;
        }
        level.gameAreas = gameAreas.ToArray();
        OnLevelSpawned?.Invoke(level);
    }

    public void DestroyLevel()
    {
        if(level != null) Destroy(level.gameObject);
    }
}
