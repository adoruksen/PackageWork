using System;
using UnityEngine;
using Random = UnityEngine.Random;

[Serializable]
public class AiSelectTargetState : SubState<AiStackState>
{
    [SerializeField] private float _brickChance;
    [SerializeField] private float _rockChance;
    [SerializeField] private float _fillChance;

    [SerializeField] private Vector2 _cooldownRange;

    private float _timer;
    private float _cooldown;

    public override void OnStateEnter(AiStackState baseState, CharacterController controller)
    {
        ResetTimer();
    }

    public override void OnStateFixedUpdate(AiStackState baseState, CharacterController controller)
    {
        if (_timer <_cooldown)
        {
            _timer += Time.fixedDeltaTime;
            return;
        }
        TryGetTarget(baseState);
    }

    private void TryGetTarget(AiStackState baseState)
    {
        var chance = Random.value;
        var min = 0f;
        if (chance> min + _brickChance)
        {
            baseState.SetState(baseState.collectState);
            return;
        }
        min += _brickChance;
        if (chance < min + _rockChance)
        {
            baseState.SetState(baseState.breakRockState);
        }
        min += _rockChance;
        if (chance <min+_fillChance)
        {
            baseState.SetState(baseState.fillAreaState);
            return;
        }
        ResetTimer();
    }

    private void ResetTimer()
    {
        _cooldown = Random.Range(_cooldownRange.x, _cooldownRange.y);
        _timer = 0f;
    }


}
