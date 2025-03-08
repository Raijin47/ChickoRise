using System.Collections;
using UnityEngine;

public class StatePlanning : IState
{
    private readonly PlayerBase Player;

    private readonly float LimitAngle = 20f;

    private float _angle;

    private float _speed;

    public StatePlanning() => Player = PlayerBase.Instance;

    public void Enter()
    {
        Player.Collider.isTrigger = false;
        Player.TrailFX.Play();
        _speed = Player.Speed.FlySpeed * (1 + Game.Data.Saves.Upgrades[1] * 0.1f);
        Game.Locator.Factory.StartHard();
    }

    public void Update()
    {
        _angle = Player.Input.Horizontal == 0 ? 0 : Player.Input.Horizontal > 0 ? -LimitAngle : LimitAngle;

        Player.Pivot.localRotation = Quaternion.Lerp(Player.Pivot.localRotation, 
            Quaternion.Euler(new Vector3(0, 0, _angle)), 
            Time.deltaTime * Player.Speed.RotateSpeed);
    }

    public IEnumerator Coroutine()
    {
        yield return null;
    }

    public void FixedUpdate()
    {
        float horizontal = Player.Input.Horizontal * Player.Speed.HorizontalFlySpeed;

        Vector3 direction = new(horizontal, Player.Speed.FallSpeed, _speed);

        Player.Rigidbody.velocity = Time.fixedDeltaTime * direction;
    }

    public void ApplyDamage() 
    { 
        Player.ChangeState(Player.StateLose);
    }

    public void OnCollisionEnter(Collision collision) 
    {
        Player.ChangeState(Player.StateLose);
    }

    public void Exit()
    {
        Player.TrailFX.Stop();
    }
}