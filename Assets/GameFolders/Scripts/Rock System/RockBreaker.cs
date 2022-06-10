using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class RockBreaker : MonoBehaviour
{
    private CharacterController _controller;
    private CharacterAnimationController _animatorController;
    private float _timer;
    [ShowInInspector] private List<Rock> _focusedRocks = new List<Rock>();
    [SerializeField] private float _cooldown;
    [SerializeField] private float _animationHitDelay;

    private void Awake()
    {
        _controller = GetComponentInParent<CharacterController>();
        _animatorController = GetComponentInParent<CharacterAnimationController>();
    }

    private void FixedUpdate()
    {
        _timer += Time.fixedDeltaTime;
        if (_timer > _cooldown) TryBreakRock();
    }

    private void TryBreakRock()
    {
        if (_focusedRocks.Count <= 0) return;

        _focusedRocks[0].Hit();
        if (_focusedRocks[0].Health <= 0) _focusedRocks.RemoveAt(0);
    }

    private void OnTriggerEnter(Collider other)
    {
        var isRock = other.TryGetComponent<Rock>(out var rock);
        if (!isRock) return;

        var index = _focusedRocks.IndexOf(rock);
        if (index == -1) return;

        _focusedRocks.RemoveAt(index);
    }
}
