using UnityEngine;
using UnityEngine.Animations.Rigging;

public class PlayerAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    private RigBuilder _rigBuilder;

    private bool _ikActive;
    [SerializeField] private Transform[] _ikNode;
    private readonly Transform[] _ikTarget = new Transform[2];

    private static readonly int Speed = Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigBuilder = GetComponentInChildren<RigBuilder>();
        _rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        _animator.SetFloat(Speed, _rigidbody.velocity.magnitude);

        if (!_ikActive) return;
        _ikNode[0].position = _ikTarget[0].position;
        _ikNode[1].position = _ikTarget[1].position;
    }

}
