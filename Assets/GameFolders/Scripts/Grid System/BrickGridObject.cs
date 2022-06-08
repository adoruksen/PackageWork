public class BrickGridObject : GridObject
{
    private Brick _brick;

    private void Awake()
    {
        _brick = GetComponent<Brick>();
    }

    private void OnEnable()
    {
        _brick.OnCollected += RemoveFromGrid;
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        _brick.OnCollected -= RemoveFromGrid;
    }
}
