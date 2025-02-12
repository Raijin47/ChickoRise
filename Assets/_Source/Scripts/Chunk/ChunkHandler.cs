using UnityEngine;

public class ChunkHandler : MonoBehaviour
{
    private Vector3 _currentSpawnPoint = new(0, 0, 643);
    private readonly Vector3 Offset = new(0, 0, 216);

    public void ChangePosition(ChunkTrigger chunk)
    {
        chunk.Chunk.position = _currentSpawnPoint;
        _currentSpawnPoint += Offset;
    }
}