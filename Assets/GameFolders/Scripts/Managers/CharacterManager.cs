using System;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class CharacterManager : MonoBehaviour
{
    public static CharacterManager instance;

    public static event Action OnCharacterSpawned;

    [SerializeField] private CharacterController _playerCharacter;
    [SerializeField] private CharacterController _aiCharacter;
    [SerializeField] private float _spacing;

    private List<CharacterController> _characters = new List<CharacterController> ();
    [ReadOnly] public CharacterController player;
    [SerializeField] private int _playerIndex;

    private void Awake()
    {
        instance = this;    
    }

    private void OnEnable()
    {
        GameManager.OnLevelStart += StartCharacters;
    }
    private void OnDisable()
    {
        GameManager.OnLevelStart -= StartCharacters;
    }

    public void SpawnCharacters(Team[] teams)
    {
        var offset = -((teams.Length - 1f) / 2) * _spacing;
        for (var i = 0; i < teams.Length; i++)
        {
            var position = Vector3.left * (offset + i * _spacing);
            if (i==_playerIndex)
            {
                SpawnPlayer(teams[i], position);
            }
            else
            {
                SpawnAi(teams[i], $"Character_{i}", position);
            }
        }
        OnCharacterSpawned?.Invoke();
    }

    private void SpawnPlayer(Team team, Vector3 position)
    {
        player = Instantiate(_playerCharacter, position, Quaternion.identity);
        player.AssignTeam(team);
        _characters.Add(player);
    }

    private void SpawnAi(Team team, string name , Vector3 position)
    {
        var ai = Instantiate(_aiCharacter, position, Quaternion.identity);
        ai.gameObject.name = name;
        ai.AssignTeam(team);
        _characters.Add(ai);
    }

    private void StartCharacters()
    {
        foreach (var characterController in _characters)
        {
            if (characterController.StackState == null) continue;
            characterController.SetState(characterController.StackState);
        }
    }

    public IReadOnlyList<CharacterController> GetCharacters() => _characters;

    public CharacterController GetCharacterByTeam(Team team)
    {
        for (int i = 0; i < _characters.Count; i++)
        {
            if (_characters[i].Team == team) return _characters[i];
        }

        Debug.Log($"Character with {team.name} Does Not Exist !", this);
        return null;
    }

    public void DestroyCharacter()
    {
        while (_characters.Count > 0)
        {
            var character = _characters[0];
            _characters.RemoveAt(0);
            Destroy(character.gameObject);
        }
    }
}
