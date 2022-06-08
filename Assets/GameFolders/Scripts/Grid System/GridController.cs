using System;
using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GridController : MonoBehaviour
{
    [SerializeField] private Vector2Int _size;
    [SerializeField] private Vector2 _space;

    private Vector3[] _slots;
    [ShowInInspector, ReadOnly] private List<int> _emptySlots;
    public int Count { get; private set; }

    public void InitializeGrid()
    {
        Count = _size.x * _size.y;
        _slots = new Vector3[Count];
        for (var i = 0; i < Count; i++)
        {
            var x = i % _size.x;
            var z = (i - x) / _size.x;
            _slots[i] = transform.position + GetSlotOffset(x, z);
        }
        _emptySlots = new List<int>();
        for (var i = 0; i < Count; i++)
        {
            _emptySlots.Add(i);
        }
    }

    public IReadOnlyList<int> GetEmptySlotIndices()
    {
        return _emptySlots;
    }

    public void AddItemToSlot(GridObject item, int index)
    {
        var slot = _slots[index];
        _emptySlots.Remove(index);
        item.PlaceOnGrid(slot);
        item.OnGridObjectUsed += EmptySlot;

        void EmptySlot()
        {
            _emptySlots.Add(index);
            item.OnGridObjectUsed -= EmptySlot;
        }
    }

    private Vector3 GetSlotOffset(int x, int z)
    {
        var xOffset = (-((float)(_size.x - 1) / 2) + x) * _space.x;
        var zOffset = (-((float)(_size.y - 1) / 2) + z) * _space.y;
        return new Vector3(xOffset, 0, zOffset);    
    }

}
