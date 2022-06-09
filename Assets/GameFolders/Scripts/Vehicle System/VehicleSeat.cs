using System;
using UnityEngine;

public class VehicleSeat : MonoBehaviour
{
    public event Action<CharacterController> OnCharacterAdded;
    public event Action<CharacterController> OnCharacterRemoved;

    [SerializeField] private bool _disableModel;

    private CharacterController _currentCharacter;
    private Transform _transform;
    public Vector3 Position => _transform.position;

    private void Awake()
    {
        _transform = transform;
    }

    private void FixedUpdate()
    {
        if (_currentCharacter == null) return;
        _currentCharacter.transform.position = _transform.position;
        _currentCharacter.transform.rotation = _transform.rotation;
    }

    public void AddCharacter(CharacterController character)
    {
        _currentCharacter = character;
        if (_disableModel) _currentCharacter.Animation.SetModelActive(false);
        OnCharacterAdded?.Invoke(_currentCharacter);
    }

    public void RemoveCharacter()
    {
        if (_currentCharacter == null) return;
        if (_disableModel) _currentCharacter.Animation.SetModelActive(true);
        OnCharacterRemoved?.Invoke(_currentCharacter);
        _currentCharacter = null;
    }
}
