using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Sirenix.OdinInspector;
using Random = UnityEngine.Random;

public class StackAreaManager : GameAreaManager
{
    [SerializeField] private GridController _grid;
    [SerializeField] private Transform _fillAreaPlacer;

    private List<Team> _teams;
    [SerializeField, BoxGroup("", false)] private StackFillAreaGenerator _fillAreaGenerator;

    [SerializeField] private int _rockAmount;

    private bool _spawnNewObjects;
    [SerializeField] private float _waitDelay;
    private WaitForSeconds _wait;
    private List<StackFillArea> _activeFillAreas;
    private List<BrickRespawner> _brickRespawners;

    private void Awake()
    {
        _wait = new WaitForSeconds(_waitDelay);
        _teams = new List<Team>();
    }

    [Button]

    public void InitializeStackArea(VehicleData[] vehicles)
    {
        _grid.InitializeGrid();
        _activeFillAreas = _fillAreaGenerator.Generate(_fillAreaPlacer, vehicles);
        _brickRespawners = new List<BrickRespawner>();
        for (var i = 0; i < _activeFillAreas.Count; i++)
        {
            _brickRespawners.Add(new BrickRespawner(this, _activeFillAreas[i]));
        }
    }

    public override void OnCharacterEntered(CharacterController player)
    {
        AddTeam(player.Team);
        if (!GameManager.instance.isPlaying) return;

        player.SetState(player.StackState);
        player.Bumper.isActive = true;
    }

    public override void OnCharacterExited(CharacterController player)
    {
        RemoveTeam(player.Team);
        CheckFail();
    }

    private void AddTeam(Team team)
    {
        _teams.Add(team);
        var characterCount = CharacterManager.instance.GetCharacters().Count;
        SpawnTeamObjects(team, _rockAmount, (_grid.Count / characterCount) - _rockAmount);
    }

    private void RemoveTeam(Team team)
    {
        _teams.Remove(team);
        var bricks = BrickManager.instance.GetAvailableObjects(team);
        while (bricks.Count>0)
        {
            BrickManager.instance.RemoveObject(bricks[0]);
        }
        var rocks = RockManager.instance.GetAvailableObjects(team);
        while (rocks.Count >0)
        {
            RockManager.instance.RemoveObject(rocks[0]);
        }
    }

    private void CheckFail()
    {
        if (_activeFillAreas.Any(fillArea => !fillArea.Filled)) return;

        foreach (var team in _teams)
        {
            var character = CharacterManager.instance.GetCharacterByTeam(team);
            if (character != CharacterManager.instance.player) continue;

            FinishManager.instance.FinishLevel();
            return;
        }
    }

    private void SpawnTeamObjects(Team team, int rockAmount, int brickAmount)
    {
        var emptySlots = _grid.GetEmptySlotIndices();
        if (emptySlots.Count <= 0) return;
        for (var i = 0; i < rockAmount; i++)
        {
            var index = Random.Range(0, emptySlots.Count);
            var rock = BrickManager.instance.SpawnObject(team);
            _grid.AddItemToSlot(rock.GetComponent<GridObject>(), emptySlots[index]);
        }

        for (int i = 0; i < brickAmount; i++)
        {
            var index = Random.Range(0, emptySlots.Count);
            var brick = BrickManager.instance.SpawnObject(team);
            _grid.AddItemToSlot(brick.GetComponent<GridObject>(), emptySlots[index]);
        }
    }

    public void SpawnBrick(Team team)
    {
        var emptySlots = _grid.GetEmptySlotIndices();
        if(emptySlots.Count <= 0 || _teams.Count <=0) return;

        var slotIndex = Random.Range(0, emptySlots.Count);
        var brick = BrickManager.instance.SpawnObject(team);

        _grid.AddItemToSlot(brick.GetComponent<GridObject>(), emptySlots[slotIndex]);
    }

    private class BrickRespawner
    {
        private StackAreaManager _manager;
        private StackFillArea _fillArea;

        public BrickRespawner(StackAreaManager manager, StackFillArea fillArea)
        {
            _manager = manager;
            _fillArea = fillArea;
            RegisterFillArea();
        }

        private void RegisterFillArea()
        {
            _fillArea.OnAdded += SpawnBrickOnGrid;
            _fillArea.OnCompleted += UnregisterFillArea;
        }

        private void UnregisterFillArea(Team team)
        {
            _fillArea.OnAdded -= SpawnBrickOnGrid;
            _fillArea.OnCompleted -= UnregisterFillArea;
        }

        private void SpawnBrickOnGrid(int index, Team team)
        {
            _manager.SpawnBrick(team);
        }
    }
}
