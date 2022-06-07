using UnityEngine;
using System.Collections.Generic;

public class Interactor : MonoBehaviour
{
    private IInteractor _controller;
    private readonly List<IStayInteract> _stayInteractables = new List<IStayInteract>();
    public bool canInteract;

    private void Awake()
    {
        _controller = GetComponentInParent<IInteractor>();
    }

    private void FixedUpdate()
    {
        if (!canInteract || !GameManager.instance.isPlaying) return;
        for (var i = 0; i < _stayInteractables.Count; i++)
        {
            if (!_stayInteractables[i].IsInteractable)
            {
                _stayInteractables.RemoveAt(i);
                continue;
            }
            _stayInteractables[i].OnInteractStay(_controller);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter");
        if (!canInteract || !GameManager.instance.isPlaying) return;
        if (!other.CompareTag(tag)) return; //objeye herhangi bir tag verilmemiþse dön

        Debug.Log("carpýsma var");
        var hasStayInteractable = other.TryGetComponent<IStayInteract>(out var stayInteract);
        if (hasStayInteractable && stayInteract.IsInteractable) _stayInteractables.Add(stayInteract);

        var hasEndInteractable = other.TryGetComponent<IBeginInteract>(out var interactable);
        if (hasEndInteractable && interactable.IsInteractable) interactable.OnInteractBegin(_controller);
    }
    private void OnTriggerExit(Collider other)
    {
        if (!canInteract || !GameManager.instance.isPlaying) return;
        if (!other.CompareTag(tag)) return; //objeye herhangi bir tag verilmemiþse dön

        var hasStayInteractable = other.TryGetComponent<IStayInteract>(out var stayInteract);
        if (hasStayInteractable && _stayInteractables.Contains(stayInteract))
            _stayInteractables.Remove(stayInteract);

        var hasEndInteractable = other.TryGetComponent<IEndInteract>(out var endInteract);
        if (hasEndInteractable && endInteract.IsInteractable) endInteract.OnInteractEnd(_controller);
    }
}
