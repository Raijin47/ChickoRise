using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private ProjectileBase _projectile;
    [SerializeField] private PlayerProjectile _lightball;
    [SerializeField] private Toadstool _toadstool;

    private Coroutine _coroutine;
    private Pool _poolProjectile;
    private Pool _poolToadstool;
    private Pool _poolLightball;

    private readonly WaitForSeconds IntervalSpawn = new(7.5f);

    private void Start()
    {
        _poolProjectile = new(_projectile);
        _poolToadstool = new(_toadstool);
        _poolLightball = new(_lightball);

        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnLose += Release;
    }

    private void Action_OnEnter()
    {
        Release();
        _coroutine = StartCoroutine(SpawnToadstoolProcess());
    }

    public void SpawnProjectile(Vector3 pos)
    {
        var projectile = _poolProjectile.Spawn(pos);

        projectile.Die += Member_Die;
    }

    public void SpawnLightball(Vector3 pos, Vector3 target)
    {
        var lightball = _poolLightball.Spawn(pos);
        lightball.transform.LookAt(target);

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
        Vector3 pos = new(Random.Range(-7.5f, 7.5f), 0, Game.Locator.Target.position.z + 300);

        var toadstool = _poolToadstool.Spawn(pos);

        toadstool.Die += Member_Die;
    }

    private void Member_Die(PoolMember obj)
    {
        
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