using System.Collections;
using UnityEngine;

public class ProjectileBase : PoolMember
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
        while (transform.position.z > Game.Locator.Player.position.z - 100)
        {
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
        if(other.TryGetComponent(out PlayerBase _))
            Game.Action.SendLose();
    }
}