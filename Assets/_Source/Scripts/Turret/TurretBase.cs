using System.Collections;
using UnityEngine;

public class TurretBase : PoolMember
{
    [SerializeField] private Transform _tower;
    [SerializeField] private Transform _spawnPoint;

    private Coroutine _coroutine;

    private readonly WaitForSeconds Interval = new(2f);

    public override void Init() => Resurrect();

    public override void Resurrect()
    {
        ReleaseCoroutine();
        _coroutine = StartCoroutine(UpdateProcess());
    }
    
    private IEnumerator UpdateProcess()
    {
        while (transform.position.z > Game.Locator.Player.position.z + 100)
        {
            yield return Interval;

            _tower.LookAt(Game.Locator.Player.position);
            SpawnBullet();
        }

        ReturnToPool();
    }

    private void SpawnBullet() => Game.Locator.Factory.SpawnProjectile(_spawnPoint);

    private void ReleaseCoroutine()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}