public class RockManager : PooledObjectManager<Rock>
{
    public static RockManager instance;

    protected override void Awake()
    {
        instance = this;
        base.Awake();
    }
    protected override void OnObjectSpawned(Rock obj)
    {
        SetObjectAvailable(obj, true);
    }
}
