using UnityEngine;

public class RockGridObject : GridObject
{
    private Rock _rock;
    private void Awake()
    {
        _rock=GetComponent<Rock>();
    }

    private void OnEnable()
    {
        _rock.OnBroken += RemoveFromGrid;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _rock.OnBroken -= RemoveFromGrid;
    }
}
