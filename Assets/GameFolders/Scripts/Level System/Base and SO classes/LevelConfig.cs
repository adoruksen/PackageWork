using UnityEngine;

[CreateAssetMenu(fileName ="Level",menuName ="Game/Level",order =0)]
public class LevelConfig : ScriptableObject
{
    [SerializeReference] public LevelPart[] Parts;
}
