using System.Collections;
using UnityEngine;

public class Fireball : PoolMember
{
    [SerializeField] private ParticleSystem _particle;

    private const float _speed = 50f;
    private Coroutine _coroutine;

    public override void Init() => Resurrect();

    public override void Resurrect()
    {
        transform.LookAt(Game.Locator.Target);

        ReleaseCoroutine();

        _coroutine = StartCoroutine(MovementProcess());
    }

    private IEnumerator MovementProcess()
    {
        while (transform.position.z > Game.Locator.Target.position.z)
        {
            Vector3 direction = Game.Locator.Target.position - transform.position;
            Quaternion rotation = Quaternion.LookRotation(direction);

            transform.rotation = Quaternion.Lerp(transform.rotation, rotation, Time.deltaTime);
            transform.position += _speed * Time.deltaTime * transform.forward;

            yield return null;
        }

        var lifeTime = 5f;

        while(lifeTime > 0)
        {
            lifeTime -= Time.deltaTime;
            transform.position += _speed * Time.deltaTime * transform.forward;

            yield return null;
        }

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

    private void OnTriggerEnter(Collider other)
    {
        Game.Locator.Player.ApplyDamage();
        ReturnToPool();
    }
}