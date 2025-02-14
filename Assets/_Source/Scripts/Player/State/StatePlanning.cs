using System.Collections;
using UnityEngine;

public class StatePlanning : IState
{
    private readonly PlayerBase Player;

    private readonly float LimitAngle = 20f;

    private float _angle;

    public StatePlanning() => Player = PlayerBase.Instance;

    public void Enter()
    {
        Player.Collider.isTrigger = false;
        Player.TrailFX.Play();
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

        Vector3 direction = new(horizontal, Player.Speed.FallSpeed, Player.Speed.FlySpeed);

        Player.Rigidbody.velocity = Time.fixedDeltaTime * direction;
    }

    public void ApplyDamage() { }

    public void OnCollisionEnter(Collision collision) 
    {
        Player.ChangeState(Player.StateLose);
    }

    public void Exit()
    {
        Player.TrailFX.Stop();
    }
}