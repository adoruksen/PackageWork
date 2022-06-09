using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class FinishManager : MonoBehaviour
{
    public static FinishManager instance;

    public IReadOnlyList<CharacterController> RankedList => _rankedList;
    private List<CharacterController> _rankedList;

    private void Awake()
    {
        instance = this;
    }

    [Button]
    public void FinishLevel()
    {
        var characters = new List<CharacterController>(CharacterManager.instance.GetCharacters());
        _rankedList = OrderList(characters);
        if (_rankedList[0] == CharacterManager.instance.player)
            GameManager.instance.CompleteGameMode();
        else
            GameManager.instance.FailGameMode();
    }

    private List<CharacterController> OrderList(List<CharacterController> characters)
    {
        var orderedList = new List<CharacterController>();
        for (var i = 0; i < characters.Count; i++)
        {
            var added = false;
            for (var j = 0; j < orderedList.Count; j++)
            {
                if (characters[i].Rigidbody.position.z <= orderedList[j].Rigidbody.position.z) continue;

                orderedList.Insert(j, characters[i]);
                added = true;
                break;
            }
            if (added) continue;

            orderedList.Add(characters[i]);
        }

        return orderedList;
    }
}
