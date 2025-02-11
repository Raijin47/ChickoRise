using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Factory : MonoBehaviour
{
    [SerializeField] private ProjectileBase _projectile;
    [SerializeField] private TurretBase _turret;

    private Coroutine _coroutine;
    private Pool _poolProjectile;
    private Pool _poolTurret;

    private readonly WaitForSeconds Interval = new(2f);

    private void Start()
    {
        _poolProjectile = new(_projectile);
        _poolTurret = new(_turret);

        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnLose += Release;
    }

    private void Action_OnEnter()
    {
        Release();
        _coroutine = StartCoroutine(SpawnTurretProcess());
    }

    public void SpawnProjectile(Transform pos)
    {
        var projectile = _poolProjectile.Spawn(pos.position);

        projectile.transform.rotation = pos.rotation;
    }

    private IEnumerator SpawnTurretProcess()
    {
        while (true)
        {
            SpawnTurret();
            yield return Interval;
        }
    }

    private void SpawnTurret()
    {
        Vector3 pos = new(Random.Range(-7, 7), 0, Game.Locator.Player.position.z + 300);

        var turret = _poolTurret.Spawn(pos);

        turret.Die += Member_Die;
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