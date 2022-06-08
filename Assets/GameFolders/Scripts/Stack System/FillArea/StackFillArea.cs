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

    //public void Add
}
