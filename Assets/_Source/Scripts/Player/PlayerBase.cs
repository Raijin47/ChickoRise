using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private readonly string AxisHorizontal = "Horizontal";
    private readonly string AxisVertical = "Vertical";

    [SerializeField] private Transform _pivot;
    [SerializeField] private Transform _view;

    private Rigidbody _rigidbody;
    private bool _isActive;
    private float _targetPosition;
    private readonly float _speedRotate = 50f;
    private readonly float _speedMovement = 50f;
    private bool IsActive
    {
        get => _isActive;
        set
        {
            _isActive = value;

            //_rigidbody.useGravity = value;
        }
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();

        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;
    }

    private void Update()
    {
        if (!IsActive) return;

        float horizontalInput = -Input.GetAxis(AxisHorizontal);

        transform.Rotate(0, horizontalInput * Time.deltaTime * _speedRotate, 0);


        _targetPosition += Input.GetAxis(AxisVertical);
        _targetPosition = Mathf.Clamp(_targetPosition, -5, -2);

        Vector3 target = new(0, 0, _targetPosition);

        _pivot.localPosition = Vector3.MoveTowards(_pivot.localPosition, target, Time.deltaTime * 50f);
    }


    private void Action_OnExit()
    {
        IsActive = false;

        transform.position = Vector3.zero;
        _pivot.localRotation = Quaternion.Euler(Vector3.zero);
        _view.localPosition = Vector3.down;
    }

    private void Action_OnEnter()
    {
        IsActive = true;
    }
}
