using System;
using UnityEngine;

public class GridObject : MonoBehaviour
{
    public bool IsInGrid { get; protected set; }
    public Action OnGridObjectUsed { get; set; }

    protected virtual void OnDisable()
    {
        RemoveFromGrid();
    }

    public void PlaceOnGrid(Vector3 position)
    {
        transform.position = position;
        IsInGrid = true;
    }

    protected void RemoveFromGrid()
    {
        if (!IsInGrid) return;
        IsInGrid = false;
    }
}
