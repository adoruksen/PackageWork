using UnityEngine;

public class InputHandleUiController : MonoBehaviour
{
    [SerializeField] private RectTransform _handle;
    [SerializeField] private RectTransform _rim;
    private Canvas _canvas;
    private bool _isActive;

    private void Awake()
    {
        _canvas = GetComponent<Canvas>();
    }

    private void Start()
    {
        RuffGameManager.OnLevelStart += EnableHandle;
        GameManager.OnGameEnd += DisableHandle;
        InputManager.Module.OnMouseDown += ShowHandle;
        InputManager.Module.OnMouseHold += UpdateHandle;
        InputManager.Module.OnMouseUp += HideHandle;
    }

    private void OnDestroy()
    {
        RuffGameManager.OnLevelStart -= EnableHandle;
        GameManager.OnGameEnd -= DisableHandle;
        InputManager.Module.OnMouseDown -= ShowHandle;
        InputManager.Module.OnMouseHold -= UpdateHandle;
        InputManager.Module.OnMouseUp -= HideHandle;
    }

    private void EnableHandle()
    {
        _isActive = true;
    }

    private void DisableHandle()
    {
        _isActive = false;
        HideHandle(Vector2.zero);
    }

    private void ShowHandle(Vector2 inputPosition)
    {
        if (!_isActive) return;
        _canvas.enabled = true;
        _rim.position = inputPosition;
    }

    private void UpdateHandle(Vector2 inputPosition)
    {
        Debug.Log("girdi");
        if (!_isActive) return;
        Debug.Log("active");

        _rim.position = InputManager.Module.DownPosition;
        _handle.localPosition = InputManager.Module.NormalizedOffset * InputManager.Module.MaxOffset;
        Debug.Log(_handle.localPosition);


    }

    private void HideHandle(Vector2 inputPosition)
    {
        _canvas.enabled = false;
    }
}
