using System;
using UnityEngine;
using Sirenix.OdinInspector;

public class StackFillArea : MonoBehaviour
{
    public event Action<int> OnAdded;
    //public event Action<bool> OnCompleted;

    [ShowInInspector, ReadOnly] private int _size;
    public bool Filled { get; private set; }
    public int Size => _size;

    private void Awake()
    {
        _size = 20;
    }

    public void AddStack()
    {
        for (int i = 0; i < Size; i++)
        {
            OnAdded?.Invoke(i);
            if (i < Size - 1) return;
            Filled = true;
        }

    }

}
