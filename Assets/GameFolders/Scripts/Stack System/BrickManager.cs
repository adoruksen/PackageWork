
public class BrickManager : PooledObjectManager<Brick>
{
    public static BrickManager instance;
    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }

    protected override void OnObjectSpawned(Brick obj)
    {
        obj.SetInteractable(true);
    }
}
