using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ObjectPool<T> where T:Object
{
    private readonly T _baseObject;
    [ShowInInspector] private readonly Queue<T> _queue = new Queue<T>();

    public ObjectPool(T baseObject)
    {
        _baseObject = baseObject;
    }
    
    public T Get()
    {
        return _queue.Count > 0 ? _queue.Dequeue() : Object.Instantiate(_baseObject);
    }

    public void Return(T t)
    {
        _queue.Enqueue(t);
    }
}
