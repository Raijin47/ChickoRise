using System.Collections;
using UnityEngine;

public class StateRacing : IState
{
    private readonly PlayerBase Player;
    private readonly float LimitAngle = 10f;

    private float _angle;

    public StateRacing() => Player = PlayerBase.Instance;

    public void Enter()
    {
        Game.Action.SendStart();
    }

    public void Update()
    {
        _angle = Player.Input.Horizontal == 0 ? 0 : Player.Input.Horizontal > 0 ? -LimitAngle : LimitAngle;

        Player.Pivot.localRotation = Quaternion.Lerp(Player.Pivot.localRotation,
            Quaternion.Euler(new Vector3(0, -_angle, 0)),
            Time.deltaTime * Player.Speed.RotateSpeed);
    }

    public IEnumerator Coroutine()
    {
        yield return null;
    }

    public void FixedUpdate()
    {
        float horizontal = Player.Input.Horizontal * Player.Speed.HorizontalGroundSpeed;

        Vector3 direction = new(horizontal, 0, Player.Speed.FastGroundSpeed);

        Player.Rigidbody.velocity = Time.fixedDeltaTime * direction;
    }

    public void ApplyDamage()
    {
        Player.ParticleTakeDamage.Play();
        Player.ChangeState(Player.StateRise);
    }
    public void OnCollisionEnter(Collision collision) { }
    public void Exit()
    {
        Player.RacingSkin.SetActive(false);
        Game.Locator.Statistic.StartPlanning = Player.Transform.position.z;
    }
}