using UnityEngine;

public class GameAreaManager : MonoBehaviour
{
    [SerializeField] private Transform _nextAreaPlacer;

    public Bounds PlayArea => _playArea;
    [SerializeField] private Bounds _playArea;

    public void MoveArea(Vector3 position)
    {
        _playArea.center += position - transform.position;
        transform.position = position;
    }

    public Vector3 GetNextAreaPosition()
    {
        return _nextAreaPlacer.position;
    }

    public virtual void OnCharacterEntered(CharacterController character) { }
    public virtual void OnCharacterExited(CharacterController character) { }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = new Color(0f, 1f, 1f, 1f);
        Gizmos.DrawWireCube(PlayArea.center, PlayArea.size);
        Gizmos.color = new Color(0, 1, 1, .5f);
        Gizmos.DrawCube(PlayArea.center, PlayArea.size);
    }
}
