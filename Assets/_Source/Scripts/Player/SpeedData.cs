using UnityEngine;

[CreateAssetMenu(fileName = "speedData", menuName = "SpeedData", order = 51)]
public class SpeedData : ScriptableObject
{

    [SerializeField] private float _normalGroundSpeed;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _fastGroundSpeed;
    [SerializeField] private float _horizontalGroundSpeed;

    [Space(10)]
    [SerializeField] private float _horizontalFlySpeed;
    [SerializeField] private float _flySpeed;
    [SerializeField] private float _fallSpeed;

    [Space(10)]
    [SerializeField] private Vector3 _forceImpulse;

    public float HorizontalFlySpeed => _horizontalFlySpeed;
    public float HorizontalGroundSpeed => _horizontalGroundSpeed;
    public float RotateSpeed => _rotateSpeed;
    public float NormalCarSpeed => _normalGroundSpeed;
    public float FastGroundSpeed => _fastGroundSpeed;
    public float FallSpeed => _fallSpeed;
    public float FlySpeed => _flySpeed;
    public Vector3 ForceImpulse => _forceImpulse;
}