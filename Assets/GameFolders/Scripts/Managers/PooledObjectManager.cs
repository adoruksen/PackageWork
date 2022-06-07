using UnityEngine;
using System.Collections.Generic;
using Sirenix.OdinInspector;


public class PooledObjectManager<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private T obj;

    public Transform poolParent;

    [ShowInInspector, ReadOnly] private List<T> _availableObjects;
    [ShowInInspector, ReadOnly] private ObjectPool<T> _pool;

    protected virtual void Awake()
    {
        _availableObjects = new List<T>();
        _pool = new ObjectPool<T>(obj);
    }

    public IReadOnlyList<T> GetAvailableObjects()
    {
        return _availableObjects;
    }
    public void SetObjectAvailable(T obj, bool available)
    {
        if (available)
        {
            AddToAvailableBricks(obj);
            return;
        }

        RemoveFromAvailableBricks(obj);
    }
    public T SpawnObject()
    {
        var obj = _pool.Get();
        obj.transform.SetParent(GameManager.instance.defaultParent);
        obj.gameObject.SetActive(true);
        OnObjectSpawned(obj);
        
        return obj;
    }

    protected virtual void OnObjectSpawned(T obj) { }

    public void RemoveObject(T obj)
    {
        obj.transform.SetParent(poolParent);
        obj.gameObject.SetActive(false);
        _pool.Return(obj);
        SetObjectAvailable(obj, false);
    }

    public void RemoveAllObjects()
    {
        var objs = GetAvailableObjects();
        while (objs.Count > 0)
        {
            RemoveObject(objs[0]);
        }
    }

    private void AddToAvailableBricks(T obj)
    {
        _availableObjects.Add(obj);
    }

    private void RemoveFromAvailableBricks(T obj)
    {
        _availableObjects.Remove(obj);
    }
}
