using UnityEngine;

public class StatePlanning : IState
{
    private readonly Rigidbody Rigidbody;
    private readonly SpeedData Speed;
    private readonly InputHandler Input;
    private readonly Transform Pivot;

    private readonly float LimitAngle = 30f;

    private float _angle;

    public StatePlanning(Rigidbody rb, SpeedData data, InputHandler input, Transform pivot)
    {
        Rigidbody = rb;
        Speed = data;
        Input = input;
        Pivot = pivot;
    }

    public void Enter()
    {

    }

    public void Update()
    {
        _angle = Input.Horizontal == 0 ? 0 : Input.Horizontal > 0 ? -LimitAngle : LimitAngle;

        Pivot.localRotation = Quaternion.Lerp(Pivot.localRotation, 
            Quaternion.Euler(new Vector3(0, 0, _angle)), 
            Time.deltaTime * Speed.RotateSpeed);
    }

    public void FixedUpdate()
    {
        float gravity = Input.Vertical == 0 ? Speed.FreeFallSpeed :
            Input.Vertical > 0 ? Speed.SlowFallSpeed : Speed.FastFallSpeed;

        float horizontal = Input.Horizontal * Speed.HorizontalSpeed;

        float forward = Input.Vertical == 0 ? Speed.FreeFlySpeed :
            Input.Vertical > 0 ? Speed.FastFlySpeed : Speed.SlowFlySpeed;

        Vector3 direction = new(horizontal, gravity, forward);

        Rigidbody.velocity = Time.fixedDeltaTime * direction;
    }

    public void Exit()
    {

    }
}