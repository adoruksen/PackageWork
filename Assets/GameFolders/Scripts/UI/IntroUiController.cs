using UnityEngine;

public class IntroUiController : UIController<IntroUiController>
{
    [SerializeField] private Animation _handAnimation;
    [SerializeField] private float _speed;

    protected override void Awake()
    {
        base.Awake();
        _handAnimation.Play();
    }
}
