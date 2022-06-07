using System;
using UnityEngine;
using Sirenix.OdinInspector;


public class StackController : MonoBehaviour
{
    public event Action<Brick> OnStackAdded;
    public event Action OnStackUsed;

    [ShowInInspector,ReadOnly,PropertyOrder(-1)] public int Stack { get; private set; }

    public void AddStack(Brick obj)
    {
        Stack++;
        OnStackAdded?.Invoke(obj);
    }

    public void UseStack()
    {
        Stack--;
        OnStackUsed?.Invoke();
    }

#if UNITY_EDITOR
    [SerializeField, BoxGroup("Box", false), HorizontalGroup("Box/Debug", .5f), LabelWidth(48)] private int _amount;
    [HorizontalGroup("Box/Debug"),Button,DisableInEditorMode,LabelText("Add Stack")] public void Editor_AddStack()
    {
        var interactor = GetComponent<PlayerController>();
        for (var i = 0; i < _amount; i++)
        {
            var obj = BrickManager.instance.SpawnObject();
            obj.OnInteractBegin(interactor);
        }
    }
#endif
}
