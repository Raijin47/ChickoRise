using UnityEngine;

public class StateIdle : IState
{
    private readonly Rigidbody Rigidbody;
    private readonly SpeedData Speed;

    public StateIdle(Rigidbody rb, SpeedData data)
    {
        Rigidbody = rb;
        Speed = data;
    }

    public void Enter()
    {

    }

    public void Update()
    {

    }

    public void FixedUpdate()
    {
        Rigidbody.velocity = Speed.NormalCarSpeed * Time.fixedDeltaTime * Vector3.forward;
    }

    public void Exit()
    {

    }
}