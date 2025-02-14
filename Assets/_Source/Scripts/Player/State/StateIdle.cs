using System.Collections;
using UnityEngine;

public class StateIdle : IState
{
    private readonly PlayerBase Player;

    public StateIdle() => Player = PlayerBase.Instance;

    public void Enter()
    {
        Player.Animator.SetBool("IsGame", false);
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

    public void OnCollisionEnter(Collision collision)
    {

    }

    public void Exit()
    {
        Player.Animator.SetBool("IsGame", true);
    }
}