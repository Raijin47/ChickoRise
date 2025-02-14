using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private Fireball _projectile;
    [SerializeField] private Lightball _lightball;
    [SerializeField] private Toadstool _toadstool;

    [Space(10)]
    [SerializeField] private ParticleSystem _lightballExplosionFX;

    private readonly List<PoolMember> Members = new();

    public ParticleSystem Lightball => _lightballExplosionFX;

    private Coroutine _coroutine;
    private Pool _poolProjectile;
    private Pool _poolToadstool;
    private Pool _poolLightball;

    private readonly WaitForSeconds IntervalSpawn = new(5f);

    private void Start()
    {
        _poolProjectile = new(_projectile);
        _poolToadstool = new(_toadstool);
        _poolLightball = new(_lightball);

        Game.Action.OnStart += Action_OnStart;
        Game.Action.OnLose += Release;
        Game.Action.OnExit += Action_OnExit;
        Game.Action.OnRestart += Action_OnExit;
    }

    private void Action_OnExit()
    {
        for (int i = Members.Count - 1; i >= 0; i--)
            Members[i].ReturnToPool();
    }

    private void Action_OnStart()
    {
        Release();
        _coroutine = StartCoroutine(SpawnToadstoolProcess());
    }

    public void SpawnFireball(Vector3 pos)
    {
        var fireball = _poolProjectile.Spawn(pos);

        Game.Audio.PlayClip(2);

        Members.Add(fireball);
        fireball.Die += Member_Die;
    }

    public void SpawnLightball(Vector3 target)
    {
        var lightball = _poolLightball.Spawn(PlayerBase.Instance.ProjectileSpawnPoint);
        lightball.transform.LookAt(target);

        Game.Audio.PlayClip(3);

        Members.Add(lightball);
        lightball.Die += Member_Die;
    }

    private IEnumerator SpawnToadstoolProcess()
    {
        while (true)
        {
            SpawnToadstool();
            yield return IntervalSpawn;
        }
    }

    private void SpawnToadstool()
    {
        bool isLeft = Random.value > 0.5f;

        Vector3 pos = new(isLeft ? -9.5f : 9.5f, 0.2f, Game.Locator.Target.position.z + 300);

        var toadstool = _poolToadstool.Spawn(pos);

        toadstool.transform.rotation = Quaternion.Euler(0, isLeft ? 166 : -166, 0);

        Members.Add(toadstool);
        toadstool.Die += Member_Die;
    }

    private void Member_Die(PoolMember obj)
    {
        Members.Remove(obj);
        obj.Die -= Member_Die;
    }

    private void Release()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}