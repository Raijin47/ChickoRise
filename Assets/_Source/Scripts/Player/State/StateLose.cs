using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateLose : IState
{
    private readonly PlayerBase Player;

    public StateLose() => Player = PlayerBase.Instance;

    public void Enter()
    {
        Game.Action.SendLose();
        Player.Rigidbody.useGravity = true;
        Player.Rigidbody.constraints = RigidbodyConstraints.None;
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

    }

    public void ApplyDamage() { }
    public void OnCollisionEnter(Collision collision) { }
    public void Exit()
    {
        Player.Collider.isTrigger = true;
        Player.Rigidbody.useGravity = false;
        Player.PlanningSkin.SetActive(false);
        Player.RacingSkin.SetActive(true);

        Player.Rigidbody.constraints = RigidbodyConstraints.FreezeRotation;
        Player.Rigidbody.angularVelocity = Vector3.zero;
        Player.Rigidbody.velocity = Vector3.zero;

        Player.Transform.localPosition = new Vector3(0, 0, Player.Transform.position.z);
        Player.Transform.localRotation = Quaternion.Euler(Vector3.zero);
        Player.Pivot.localRotation = Quaternion.Euler(Vector3.zero);
    }
}
