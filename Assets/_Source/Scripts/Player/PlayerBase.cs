using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance;

    [SerializeField] private GameObject _planningSkin;
    [SerializeField] private GameObject _racingSkin;

    [SerializeField] private ParticleSystem _particleTakeDamage;
    [SerializeField] private Transform _pivot;
    [SerializeField] private SpeedData _speed;

    private IState _currentState;

    private StateIdle _stateIdle;
    private StateShooter _stateShooter;
    private StatePlanning _statePlanning;
    private StateRise _stateRise;

    public SpeedData Speed => _speed;
    public GameObject RacingSkin => _racingSkin;
    public GameObject PlanningSkin => _planningSkin;

    private void Awake() => Instance = this;

    private void Start()
    {
        var rigidbody = GetComponent<Rigidbody>();
        var Input = GetComponent<InputHandler>();

        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;

        _stateIdle = new(rigidbody, _speed);
        _stateShooter = new(rigidbody, _speed, Input, _pivot);
        _statePlanning = new(rigidbody, _speed, Input, _pivot);
        _stateRise = new(rigidbody, _speed);
        _currentState = _stateIdle;
    }

    private void Update() => _currentState.Update();
    private void FixedUpdate() =>  _currentState.FixedUpdate();

    private void Action_OnExit()
    {
        transform.localPosition = Vector3.zero;
        _pivot.localRotation = Quaternion.Euler(Vector3.zero);
    }

    private void Action_OnEnter() => ChangeState(_stateShooter);

    public void ChangeState()
    {
        if(_currentState == _stateShooter)
        {
            ChangeState(_stateRise);
            _particleTakeDamage.Play();
            return;
        }

        if (_currentState == _stateRise)
        {
            ChangeState(_statePlanning);
            return;
        }
    }

    private void ChangeState(IState state)
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }
}