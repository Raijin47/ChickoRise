using UnityEngine;

public class StateRise : IState
{
    private readonly Rigidbody Rigidbody;
    private readonly SpeedData Speed;

    private float _currentTime;
    private readonly float RequiredTime = 1f;

    public StateRise(Rigidbody rb, SpeedData data)
    {
        Rigidbody = rb;
        Speed = data;
        _currentTime = RequiredTime;
    }

    public void Enter()
    {
        PlayerBase.Instance.PlanningSkin.SetActive(true);
        Rigidbody.velocity = Vector3.zero;
        Rigidbody.useGravity = true;
        Rigidbody.AddForce(Vector3.up * 100, ForceMode.Impulse);
    }

    public void Update()
    {
        if (_currentTime > 0)
        {
            _currentTime -= Time.deltaTime;
            return;
        }

        PlayerBase.Instance.ChangeState();
    }

    public void FixedUpdate()
    {
        //if (!_isActive) return;
        //if (Rigidbody.velocity.y > 0) return;

        //PlayerBase.Instance.ChangeState();
    }

    public void Exit()
    {
        _currentTime = RequiredTime;
    }
}