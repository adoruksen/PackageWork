using Sirenix.OdinInspector;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    //assing this script to character

    private Transform _transform;

    public Vector3 Position => _transform.position;

    private void Awake()
    {
        _transform = transform;
    }

#if UNITY_EDITOR
    [Button,DisableInEditorMode]
    private void FollowThis()
    {
        CameraController.instance.SetTarget(this);
    }
#endif

}
