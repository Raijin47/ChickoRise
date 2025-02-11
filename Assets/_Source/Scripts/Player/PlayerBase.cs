using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    [SerializeField] private Transform _pivot;
    [SerializeField] private SpeedData _speed;

    private readonly float LimitAngle = 30f;

    private InputHandler _input;
    private Rigidbody _rigidbody;
    private bool _isActive;

    private float _angle;

    private bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;
            _rigidbody.velocity = Vector3.zero;
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = GetComponent<InputHandler>();

        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;
    }

    private void Update()
    {
        if (!IsActive) return;

        _angle = _input.Horizontal == 0 ? 0 : _input.Horizontal > 0 ? -LimitAngle : LimitAngle;

        _pivot.localRotation = Quaternion.Lerp(_pivot.localRotation, Quaternion.Euler(new Vector3(0, 0, _angle)), Time.deltaTime * _speed.RotateSpeed);
    }

    private void FixedUpdate()
    {
        if (!IsActive) return;

        float gravity = _input.Vertical == 0 ? _speed.FreeFallSpeed : 
            _input.Vertical > 0 ? _speed.SlowFallSpeed : _speed.FastFallSpeed;

        float horizontal = _input.Horizontal * _speed.HorizontalSpeed;

        float forward = _input.Vertical == 0 ? _speed.FreeFlySpeed : 
            _input.Vertical > 0 ? _speed.FastFlySpeed : _speed.SlowFlySpeed;

        Vector3 direction = new(horizontal, gravity, forward);

        _rigidbody.velocity = Time.fixedDeltaTime * direction;
    }

    private void Action_OnExit()
    {
        IsActive = false;

        transform.localPosition = Vector3.zero;
        _pivot.localRotation = Quaternion.Euler(Vector3.zero);
        _angle = 0;
    }

    private void Action_OnEnter()
    {
        IsActive = true;
    }
}