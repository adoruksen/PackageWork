using Sirenix.OdinInspector;
using UnityEngine;


[CreateAssetMenu(fileName = "CameraConfig", menuName = "Game/Camera/CameraConfig", order = 0)]
[InlineEditor]
public class CameraConfig : ScriptableObject
{
    public Vector3 offset;
    public float rotation;

#if UNITY_EDITOR

    [Button, DisableInEditorMode]
    private void ReadFromCamera()
    {
        offset = CameraController.instance.followTarget.Position - CameraController.instance.transform.position;
        rotation = CameraController.instance.transform.rotation.eulerAngles.x;
        UnityEditor.EditorUtility.SetDirty(this);
    }

#endif
}


