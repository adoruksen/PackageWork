using UnityEngine;

[CreateAssetMenu(menuName = "Game/Input/LockedTouch", order = -989)]
public class LockedTouchInputModule : InputModule
{
    public override void Update()
    {
        if (MouseDown)
        {
            _downPosition = MousePosition;
            _lastPosition = MousePosition;
            OnMouseDown?.Invoke(MousePosition);
        }

        if (MouseHold)
        {
            _delta = MousePosition - LastPosition;
            _delta *= Normalizer * Sensitivity;
            _offset = MousePosition - DownPosition;
            _offset *= Normalizer * Sensitivity;
            OnMouseHold?.Invoke(MousePosition);
        }

        if (MouseUp)
        {
            _delta = Vector2.zero;
            _offset = Vector2.zero;
            OnMouseUp?.Invoke(MousePosition);
        }

        _normalizedDelta = Vector2.ClampMagnitude(Delta / MaxOffset, 1f);
        _normalizedOffset = Vector2.ClampMagnitude(Offset / MaxOffset, 1f);
        _lastPosition = MousePosition;
    }
}
