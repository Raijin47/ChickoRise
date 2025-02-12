using UnityEngine;

[CreateAssetMenu(fileName = "speedData", menuName = "SpeedData", order = 51)]
public class SpeedData : ScriptableObject
{
    [SerializeField] private float _horizontalSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _normalGroundSpeed;
    [SerializeField] private float _fastGroundSpeed;
    [SerializeField] private float _horizontalGroundSpeed;

    [Space(10)]
    [SerializeField] private float _freeFallSpeed;
    [SerializeField] private float _slowFallSpeed;
    [SerializeField] private float _fastFallSpeed;

    [Space(10)]
    [SerializeField] private float _freeFlySpeed;
    [SerializeField] private float _slowFlySpeed;
    [SerializeField] private float _fastFlySpeed;

    public float HorizontalSpeed => _horizontalSpeed;
    public float HorizontalGroundSpeed => _horizontalGroundSpeed;
    public float RotateSpeed => _rotateSpeed;
    public float NormalCarSpeed => _normalGroundSpeed;
    public float FastGroundSpeed => _fastGroundSpeed;
    public float FreeFallSpeed => _freeFallSpeed;
    public float SlowFallSpeed => _slowFallSpeed;
    public float FastFallSpeed => _fastFallSpeed;
    public float FreeFlySpeed => _freeFlySpeed;
    public float SlowFlySpeed => _slowFlySpeed;
    public float FastFlySpeed => _fastFlySpeed;
}