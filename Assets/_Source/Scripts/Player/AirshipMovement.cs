using System.Collections;
using UnityEngine;

public class AirshipMovement : MonoBehaviour
{
    [SerializeField] private float _speed;
    private Coroutine _coroutine;

    private void Start()
    {
        Game.Action.OnEnter += Action_OnEnter;
        //_coroutine = StartCoroutine(MovementProcess());
        Game.Action.OnExit += Action_OnExit;
    }

    private void Action_OnEnter()
    {
        Release();
    }

    private IEnumerator MovementProcess()
    {
        while (true)
        {
            transform.position += _speed * Time.deltaTime * transform.forward;
            yield return null;
        }
    }
    private void Action_OnExit()
    {
        Release();
        _coroutine = StartCoroutine(MovementProcess());
    }

    private void Release()
    {
        if(_coroutine != null)
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}