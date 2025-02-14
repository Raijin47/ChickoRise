using System.Collections;
using UnityEngine;

public class StateIdle : IState
{
    private readonly PlayerBase Player;

    public StateIdle() => Player = PlayerBase.Instance;

    public void Enter()
    {
        Player.Transform.localPosition = Vector3.zero;
        Player.Pivot.localRotation = Quaternion.Euler(Vector3.zero);
    }

    public void Update()
    {

    }

    public IEnumerator Coroutine()
    {
        yield return null;
    }

    public void FixedUpdate()
    {
        Player.Rigidbody.velocity = Player.Speed.NormalCarSpeed * Time.fixedDeltaTime * Vector3.forward;
    }

    public void ApplyDamage() { }

    public void Exit()
    {

    }
}