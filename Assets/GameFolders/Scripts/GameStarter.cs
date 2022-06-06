using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

[TypeInfoBox("Starts the game with input on Level Initialized.")]
public class GameStarter : MonoBehaviour
{
    private List<RaycastResult> _results = new List<RaycastResult>();
    private static bool _checkInput;

    private void OnEnable()
    {
        GameManager.OnGameInitialized += CheckInput;
    }

    private void OnDisable()
    {
        GameManager.OnGameInitialized -= CheckInput;
    }

    private void Update()
    {
        if (!_checkInput) return;

        if (!Input.GetMouseButtonDown(0)) return;

        _results.Clear();
        var eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        EventSystem.current.RaycastAll(eventData, _results);
        if (_results.Count > 0) return;

        _checkInput = false;
        GameManager.instance.StartGameMode();
    }

    private void CheckInput()
    {
        DOVirtual.DelayedCall(.2f, () => _checkInput = true, false);
    }

    public static void SetCheckInput(bool isCheck)
    {
        _checkInput = isCheck;
    }
}
