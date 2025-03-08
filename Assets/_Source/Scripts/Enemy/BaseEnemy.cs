using System.Collections;
using UnityEngine;

public abstract class BaseEnemy : PoolMember
{
    [SerializeField] protected Transform _spawnPoint;

    private Coroutine _coroutine;

    private readonly WaitForSeconds Interval = new(3f);

    public override void Init() => Resurrect();

    public override void Resurrect()
    {
        Game.Action.OnLose += ReleaseCoroutine;
        ReleaseCoroutine();
        _coroutine = StartCoroutine(UpdateProcess());
    }

    private IEnumerator UpdateProcess()
    {
        while (CheckDistance())
        {
            yield return Delay();
            SpawnProjectile();
        }

        while (transform.position.z > Game.Locator.Target.position.z)
            yield return Interval;

        yield return Interval;

        ReturnToPool();
    }

    private void ReleaseCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    public override void Release()
    {
        Game.Action.OnLose -= ReleaseCoroutine;
    }

    protected abstract void SpawnProjectile();
    protected abstract bool CheckDistance();
    protected abstract WaitForSeconds Delay();
}