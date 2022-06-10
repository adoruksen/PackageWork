using UnityEngine;

public class MaterialSetter : MonoBehaviour
{
    [SerializeField] private int[] _indices;
    [SerializeField] private Renderer _renderer;

    public void SetMaterial(Material mat)
    {
        var materials = _renderer.sharedMaterials;
        foreach (var index in _indices)
        {
            materials[index] = mat;
        }
        _renderer.sharedMaterials = materials;
    }
}
