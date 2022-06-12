using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour ,IHaveTeam
{

    public event Action OnBroken;
    public event Action<Team> OnTeamChanged;

    public Team Team => _team;
    [SerializeField, DisableInPlayMode] private Team _team;

    [ShowInInspector, ReadOnly, ProgressBar(0, "$_maxHealth")] public int Health { get; private set; }
    [SerializeField] private int _maxHealth;

    [SerializeField] private int _stackAmount;
    [SerializeField] private float _verticalForce;
    [SerializeField] private float _horizontalForce;
    [SerializeField] private Transform _stackSpawnPosition;

    [SerializeField] private GameObject[] _breakPieces;

    private void Start()
    {
        if(Team!=null) OnTeamChanged?.Invoke(Team);
    }

    private void OnEnable()
    {
        Health = _maxHealth;
        foreach (var piece in _breakPieces)
        {
            piece.SetActive(true);
        }
    }
    public void AssignTeam(Team team)
    {
        if (team == Team) return;
        _team = team;
        OnTeamChanged?.Invoke(team);
    }

    public void Hit()
    {
        Health--;
        _breakPieces[Health].SetActive(false);
        if (Health <= 0) Break();
    }

    private void Break()
    {
        for (var i = 0; i < _stackAmount; i++)
        {
            var obj = BrickManager.instance.SpawnObject(Team);
            SetupBrickTransform(obj);
            FlingBrick(obj);
        }
        OnBroken?.Invoke();
        RockManager.instance.RemoveObject(this);
    }

    private void SetupBrickTransform(Component brick)
    {
        var brickTransform = brick.transform;
        brickTransform.position = _stackSpawnPosition.position;
        brickTransform.rotation = Quaternion.identity;
        brickTransform.localScale = Vector3.one;
    }

    private void FlingBrick(Component brick)
    {
        Vector3 force = Random.insideUnitCircle * _horizontalForce;
        force = new Vector3(force.x, Random.value * _verticalForce, force.y);
        var brickRigidbody = brick.GetComponent<Rigidbody>();
        brickRigidbody.AddForce(force);
        brickRigidbody.AddTorque(Random.onUnitSphere);
    }
    
}
