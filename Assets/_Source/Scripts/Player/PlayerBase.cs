using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public static PlayerBase Instance;

    [SerializeField] private Animator _animator;
    [SerializeField] private GameObject _planningSkin;
    [SerializeField] private GameObject _racingSkin;
    [SerializeField] private Transform _projectileSpawnPoint;
    [SerializeField] private ParticleSystem _particleTakeDamage;
    [SerializeField] private ParticleSystem _particleSmoke;
    [SerializeField] private ParticleSystem _particleTrail;
    [SerializeField] private Transform _pivot;
    [SerializeField] private SpeedData _speed;
    [SerializeField] private Collider _collider;

    private IState _currentState;

    private Rigidbody _rigidbody;
    private Coroutine _coroutine;
    private InputHandler _input;

    private StateIdle _stateIdle;
    private StateRacing _stateRacing;
    private StatePlanning _statePlanning;
    private StateRise _stateRise;
    private StateLose _stateLose;

    public Animator Animator => _animator;
    public ParticleSystem TrailFX => _particleTrail;
    public Transform Transform => transform;
    public StateIdle StateIdle => _stateIdle;
    public StateRacing StateRacing => _stateRacing;
    public StatePlanning StatePlanning => _statePlanning;
    public StateRise StateRise => _stateRise;
    public StateLose StateLose => _stateLose;
    public ParticleSystem ParticleSmoke => _particleSmoke;
    public ParticleSystem ParticleTakeDamage => _particleTakeDamage;
    public Rigidbody Rigidbody => _rigidbody;
    public SpeedData Speed => _speed;
    public InputHandler Input => _input;
    public Transform Pivot => _pivot;
    public GameObject RacingSkin => _racingSkin;
    public GameObject PlanningSkin => _planningSkin;
    public Vector3 ProjectileSpawnPoint => _projectileSpawnPoint.position;
    public Collider Collider => _collider;

    private void Awake() => Instance = this;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _input = GetComponent<InputHandler>();

        Game.Action.OnEnter += Action_OnEnter;
        Game.Action.OnExit += Action_OnExit;
        Game.Action.OnRestart += Action_OnRestart;

        _stateIdle = new();
        _stateRacing = new();
        _statePlanning = new();
        _stateRise = new();
        _stateLose = new();
        _currentState = _stateIdle;
    }

    private void Action_OnRestart() => ChangeState(_stateRacing);
    private void Update() => _currentState.Update();
    private void FixedUpdate() =>  _currentState.FixedUpdate();

    private void Action_OnExit() => ChangeState(_stateIdle);
    private void Action_OnEnter() => ChangeState(_stateRacing);
    public void ApplyDamage() => _currentState.ApplyDamage();

    public void ChangeState(IState state)
    {
        _currentState.Exit();
        _currentState = state;
        _currentState.Enter();
    }

    public void PlayCoroutine()
    {
        ReleaseCoroutine();
        _coroutine = StartCoroutine(_currentState.Coroutine());
    }

    private void ReleaseCoroutine()
    {
        if (_coroutine == null) return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    private void OnCollisionEnter(Collision collision) => _currentState.OnCollisionEnter(collision);
}