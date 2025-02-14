using UnityEngine;

public class ChunkTrigger : MonoBehaviour
{
    [SerializeField] private Transform _chunk;

    public Transform Chunk => _chunk;

    private void OnTriggerEnter(Collider other) => Game.Locator.Chunk.ChangePosition(this);
}