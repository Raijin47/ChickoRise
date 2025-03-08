using System.Collections;
using UnityEngine;

public class Lightball : PoolMember
{
    [SerializeField] private ParticleSystem _particle;

    private const float _speed = 100f;
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
        if (other.TryGetComponent(out BaseEnemy enemy))
        {
            Game.Audio.PlayClip(0);
            enemy.ReturnToPool();

            Game.Locator.Statistic.KilledEnemy++;
        }

        Game.Locator.Factory.Lightball.transform.position = transform.position;
        Game.Locator.Factory.Lightball.Play();

        ReturnToPool();
    }
}