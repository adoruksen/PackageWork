using DG.Tweening;
using UnityEngine;

public class StackFillAreaVisualController : MonoBehaviour
{
    private StackFillArea _fillArea;
    [SerializeField] private FillObject[] _fillSlots;
    private int _lastIndex = -1;

    private void Awake()
    {
        _fillArea=GetComponentInParent<StackFillArea>();
    }

    private void OnEnable()
    {
        _fillArea.OnAdded += UpdateGrid;
    }

    private void OnDisable()
    {
        _fillArea.OnAdded -= UpdateGrid;
    }

    private void UpdateGrid(int index)
    {
        if (_lastIndex >= index) return;

        _fillSlots[index].transform.DOScale(Vector3.one, .2f).SetEase(Ease.OutBack, 3f);
        _lastIndex++;
    }
}
