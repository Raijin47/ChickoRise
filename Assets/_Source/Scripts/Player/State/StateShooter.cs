using UnityEngine;

public class StateShooter : IState
{
    private readonly Rigidbody Rigidbody;
    private readonly SpeedData Speed;
    private readonly InputHandler Input;
    private readonly Transform Pivot;

    private readonly float LimitAngle = 10f;

    private float _angle;

    public StateShooter(Rigidbody rb, SpeedData data, InputHandler input, Transform pivot)
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
            Quaternion.Euler(new Vector3(0, -_angle, 0)),
            Time.deltaTime * Speed.RotateSpeed);
    }

    public void FixedUpdate()
    {
        //Rigidbody.velocity = Speed.FastCarSpeed * Time.fixedDeltaTime * Vector3.forward;

        float horizontal = Input.Horizontal * Speed.HorizontalGroundSpeed;

        Vector3 direction = new(horizontal, 0, Speed.FastGroundSpeed);

        Rigidbody.velocity = Time.fixedDeltaTime * direction;
    }

    public void Exit()
    {
        PlayerBase.Instance.RacingSkin.SetActive(false);
    }
}