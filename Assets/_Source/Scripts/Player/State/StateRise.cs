using System.Collections;
using UnityEngine;

public class StateRise : IState
{
    private readonly PlayerBase Player;
    private readonly WaitForSeconds Delay = new(1f);

    public StateRise() => Player = PlayerBase.Instance;

    public void Enter()
    {
        Player.PlanningSkin.SetActive(true);
        Player.Rigidbody.velocity = Vector3.zero;
        Player.ParticleSmoke.Play();
        Game.Audio.PlayClip(1);
        Player.PlayCoroutine();
    }

    public void Update() { }
    public void FixedUpdate() { }
    
    public IEnumerator Coroutine()
    {
        yield return Delay;

        Player.Rigidbody.useGravity = true;
        Player.Rigidbody.AddForce(Player.Speed.ForceImpulse, ForceMode.Impulse);

        yield return Delay;

        Player.ChangeState(Player.StatePlanning);
    }

    public void ApplyDamage() { }
    public void OnCollisionEnter(Collision collision) { }
    public void Exit()
    {
        Player.ParticleSmoke.Stop();
        Player.Rigidbody.useGravity = false;
    }
}