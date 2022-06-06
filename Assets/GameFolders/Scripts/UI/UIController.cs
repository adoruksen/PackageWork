using DG.Tweening;
using UnityEngine;

public class UIController<T> : Singleton<T> where T : MonoBehaviour
{
    protected Canvas _canvas;
    protected CanvasGroup _group;

    protected virtual void Awake()
    {
        _canvas = GetComponent<Canvas>();
        _group = GetComponent<CanvasGroup>();
    }

    public virtual void Show()
    {
        _canvas.enabled = true;
        _group.DOFade(1f, .35f).SetEase(Ease.OutQuart).OnComplete(() =>
        {
            _group.interactable = true;
        });
    }

    public virtual void Hide()
    {
        _group.interactable = false;
        _group.DOFade(0f, .35f).SetEase(Ease.InQuart).OnComplete(() =>
        {
            _canvas.enabled = false;
        });
    }
    public virtual void ShowInstant()
    {
        _canvas.enabled = true;
        _group.interactable = true;
        _group.alpha = 1f;
    }

    public virtual void HideInstant()
    {
        _canvas.enabled = false;
        _group.interactable = false;
        _group.alpha = 0f;
    }
}