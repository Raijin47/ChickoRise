using System.Collections;
using UnityEngine;

public class Toadstool : PoolMember
{
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _target;

    private Coroutine _coroutine;

    private readonly WaitForSeconds Interval = new(5f);

    public override void Init() => Resurrect();

    public override void Resurrect()
    {
        ReleaseCoroutine();
        _coroutine = StartCoroutine(UpdateProcess());
    }
    
    private IEnumerator UpdateProcess()
    {
        while (transform.position.z > Game.Locator.Target.position.z)
        {
            yield return Interval;
            Game.Locator.Factory.SpawnFireball(_spawnPoint.position);
        }

        yield return Interval;

        ReturnToPool();
    }

    private void ReleaseCoroutine()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}