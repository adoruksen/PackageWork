using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static InputManager _instance;
    public static InputModule Module => _instance._module;
    [SerializeField] private InputModule _module;

    private void Awake()
    {
        _instance = this;
    }

    private void Update()
    {
        Module.Update();
    }
}
