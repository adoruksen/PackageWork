using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAnimationController : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody _rigidbody;

    [SerializeField] private GameObject[] _model;
    private RigBuilder _rigBuilder;

    private bool _ikActive;
    [SerializeField] private Transform[] _ikNode;
    private readonly Transform[] _ikTarget = new Transform[2];

    private static readonly int Speed = Animator.StringToHash("Speed");
    private static readonly int Mine = Animator.StringToHash("Mine");
    private static readonly int Fall = Animator.StringToHash("Fall");
    private static readonly int Win = Animator.StringToHash("Win");
    private static readonly int Fail = Animator.StringToHash("Fail");
    private static readonly int VehicleEnter = Animator.StringToHash("EnterVehicle");
    private static readonly int VehicleExit = Animator.StringToHash("ExitVehicle");


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

    public void SetModelActive(bool isActive)
    {
        foreach (var model in _model)
        {
            model.SetActive(isActive);
        }
    }

    public void TriggerMine() => _animator.SetTrigger(Mine);
    public void TriggerFall() => _animator.SetTrigger(Fall);
    public void SetWin() => _animator.SetTrigger(Win);
    public void SetFail() => _animator.SetTrigger(Fail);
    public void EnterVehicle() => _animator.SetTrigger(VehicleEnter);
    public void ExitVehicle() => _animator.SetTrigger(VehicleExit);

    public void SetAnimator(AnimatorOverrideController animator) => _animator.runtimeAnimatorController = animator;

}
