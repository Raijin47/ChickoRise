using UnityEngine;

public class VampireBat : BaseEnemy
{
    private readonly WaitForSeconds IntervalSpawn = new(1f);

    protected override void SpawnProjectile()
    {
        Game.Locator.Factory.SpawnDarkball(_spawnPoint.position);
    }

    protected override bool CheckDistance()
    {
        return transform.position.z > Game.Locator.Target.position.z + 10;
    }

    protected override WaitForSeconds Delay()
    {
        return IntervalSpawn;
    }
}