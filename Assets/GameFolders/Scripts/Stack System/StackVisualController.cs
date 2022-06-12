using System.Collections.Generic;
using Sirenix.OdinInspector;
using DG.Tweening;
using UnityEngine;

public class StackVisualController : MonoBehaviour
{
    private StackController _stackController;

    [ShowInInspector] private readonly Stack<Brick> _stackedObjects = new Stack<Brick>();
    [SerializeField] private Transform _stackParent;
    [SerializeField] private float _distance;

    private void Awake()
    {
        _stackController = GetComponent<StackController>();
    }

    private void OnEnable()
    {
        _stackController.OnStackAdded += UpdateVisualAdded;
        _stackController.OnStackUsed += UpdateVisualUsed;
        _stackController.OnStackLost += UpdateVisualLostAll;
    }

    private void OnDisable()
    {
        _stackController.OnStackAdded -= UpdateVisualAdded;
        _stackController.OnStackUsed -= UpdateVisualUsed;
        _stackController.OnStackLost -= UpdateVisualLostAll;
    }

    private void UpdateVisualAdded(Brick obj)
    {
        _stackedObjects.Push(obj);
        var objTransform = obj.transform;
        objTransform.SetParent(_stackParent);
        objTransform.localPosition = Vector3.up * (_stackController.Stack * _distance);
        objTransform.localRotation = Quaternion.identity;
    }

    private void UpdateVisualUsed()
    {
        var obj = _stackedObjects.Pop();
        obj.transform.DOScale(Vector3.zero, .2f).OnComplete(() =>
        {
            BrickManager.instance.RemoveObject(obj);
            obj.transform.localScale = Vector3.one;
        });
    }

    private void UpdateVisualLostAll()
    {
        while (_stackedObjects.Count > 0)
        {
            var obj = _stackedObjects.Pop();
            obj.SetLost();
        }
    }
}
