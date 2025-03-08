using UnityEngine;

public class Toadstool : BaseEnemy
{
    private readonly WaitForSeconds IntervalSpawn = new(3f);

    protected override void SpawnProjectile()
    {
        Game.Locator.Factory.SpawnFireball(_spawnPoint.position);
    }

    protected override bool CheckDistance()
    {
        return transform.position.z > Game.Locator.Target.position.z + 20;
    }

    protected override WaitForSeconds Delay()
    {
        return IntervalSpawn;
    }
}