using UnityEngine;
using UnityEngine.UI;

public class WinUiController : UIController<WinUiController>
{
    [SerializeField] private Button _nextButton;
    [SerializeField] private GameMode _gameMode;

    private void OnEnable()
    {
        _nextButton.onClick.AddListener(NextButtonPressed);
    }

    private void OnDisable()
    {
        _nextButton.onClick.RemoveListener(NextButtonPressed);
    }

    private void NextButtonPressed()
    {
        GameManager.instance.InitializeGameMode(_gameMode);
        HideInstant();
    }
}
