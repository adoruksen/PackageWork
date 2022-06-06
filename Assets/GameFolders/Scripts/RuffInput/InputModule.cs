using System;
using Sirenix.OdinInspector;
using UnityEngine;

public abstract class InputModule : ScriptableObject
{
    public Action<Vector2> OnMouseDown;
    public Action<Vector2> OnMouseHold;
    public Action<Vector2> OnMouseUp;
    public Vector2 Normalizer = Vector2.one;

    [SerializeField] protected bool _isActive;
    public virtual bool IsActive
    {
        get => _isActive;
        set => _isActive = value;
    }

    [SerializeField] protected float _maxOffset;
    public virtual float MaxOffset
    {
        get => _maxOffset;
        set => _maxOffset = value;
    }

    [SerializeField] protected float _sensitivity;
    public virtual float Sensitivity
    {
        get => _sensitivity;
        set => _sensitivity = value;
    }

    [FoldoutGroup("Debug"), BoxGroup("Debug/Deltas", false)]
    [SerializeField, DisableIf("$ReadOnly")] protected Vector2 _delta;
    public virtual Vector2 Delta => _delta;

    [BoxGroup("Debug/Deltas", false)]
    [SerializeField, DisableIf("$ReadOnly")] protected Vector2 _offset;
    public virtual Vector2 Offset => _offset;

    [BoxGroup("Debug/Deltas", false)]
    [SerializeField, DisableIf("$ReadOnly")] protected Vector2 _normalizedDelta;
    public virtual Vector2 NormalizedDelta => _normalizedDelta;

    [BoxGroup("Debug/Deltas", false)]
    [SerializeField, DisableIf("$ReadOnly")] protected Vector2 _normalizedOffset;
    public virtual Vector2 NormalizedOffset => _normalizedOffset;

    [BoxGroup("Debug/Positions", false)]
    [SerializeField, DisableIf("$ReadOnly")] protected Vector2 _downPosition;
    public virtual Vector2 DownPosition => _downPosition;

    [BoxGroup("Debug/Positions", false)]
    [SerializeField, DisableIf("$ReadOnly")] protected Vector2 _lastPosition;
    public virtual Vector2 LastPosition => _lastPosition;

    [BoxGroup("Debug/Positions", false)]
    [SerializeField, DisableIf("$ReadOnly")] protected Vector2 _mousePosition;
    public virtual Vector2 MousePosition
    {
        get
        {
            _mousePosition = Input.mousePosition;
            return _mousePosition;
        }
    }

    [BoxGroup("Debug/Bools", false)]
    [ShowInInspector, DisableIf("$ReadOnly")] public virtual bool MouseDown => Input.GetMouseButtonDown(0);

    [BoxGroup("Debug/Bools", false)]
    [ShowInInspector, DisableIf("$ReadOnly")] public virtual bool MouseHold => Input.GetMouseButton(0);

    [BoxGroup("Debug/Bools", false)]
    [ShowInInspector, DisableIf("$ReadOnly")] public virtual bool MouseUp => Input.GetMouseButtonUp(0);

    public abstract void Update();

    public void GetNormalizer()
    {
        Normalizer = InputNormalizer.GetNormalizer();
    }

    private const bool ReadOnly = true;
}
