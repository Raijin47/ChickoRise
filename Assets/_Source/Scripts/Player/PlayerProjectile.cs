using System.Collections;
using UnityEngine;

public class PlayerProjectile : PoolMember
{
    [SerializeField] private ParticleSystem _particle;

    private const float _speed = 50f;
    private Coroutine _coroutine;

    public override void Init() => Resurrect();

    public override void Resurrect()
    {
        ReleaseCoroutine();
        _coroutine = StartCoroutine(MovementProcess());
    }

    private IEnumerator MovementProcess()
    {
        while (true)
        {
            transform.position += _speed * Time.deltaTime * transform.forward;
            yield return null;
        }
    }

    private void ReleaseCoroutine()
    {
        if (_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Toadstool enemy))
            enemy.ReturnToPool();

        ReturnToPool();
    }
}