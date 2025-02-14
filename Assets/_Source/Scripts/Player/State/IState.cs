using System.Collections;
using UnityEngine;

public interface IState
{
    public void Enter();
    public void Update();
    public IEnumerator Coroutine();
    public void FixedUpdate();
    public void ApplyDamage();
    public void Exit();
    public void OnCollisionEnter(Collision collision);
}