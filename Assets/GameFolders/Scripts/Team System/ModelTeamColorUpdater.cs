using UnityEngine;

public class ModelTeamColorUpdater : MonoBehaviour
{
    private IHaveTeam _controller;
    private MaterialSetter[] _materialSetters;
    [SerializeField] private TeamMaterialGroup.Type _type;

    private void Awake()
    {
        _controller = GetComponentInParent<IHaveTeam>();
        _materialSetters = GetComponentsInChildren<MaterialSetter>();
        _controller.OnTeamChanged += UpdateColor;
    }

    private void OnDestroy()
    {
        _controller.OnTeamChanged -= UpdateColor;
    }

    private void UpdateColor(Team newTeam)
    {
        var material = SharedMaterialManager.instance.GetTeamMaterial(newTeam, _type);
        foreach (var materialSetter in _materialSetters)
        {
            materialSetter.SetMaterial(material);
        }
    }
}
