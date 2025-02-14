using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatistic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDistance;
    [SerializeField] private TextMeshProUGUI _textResult;

    private Transform _player;

    private float _distance;
    private float _startRace;
    private int _killedEnemy;
    public int KilledEnemy
    {
        get => _killedEnemy;
        set
        {
            _killedEnemy = value;
        }
    }

    private bool _isActive;

    public float StartRace { get => _startRace; set { _startRace = value;  _isActive = true; } }
    public float StartPlanning { get; set; }

    private float Distance
    {
        get => _distance;
        set
        {
            _distance = Mathf.Floor(value);
            _textDistance.text = $"{_distance}m";
        }
    }


    private void Start()
    {
        _player = PlayerBase.Instance.transform;
        Game.Action.OnLose += Action_OnLose;
        Game.Action.OnStart += Action_OnStart;
    }

    private void Action_OnStart()
    {
        StartRace = _player.transform.position.z;
    }

    private void Action_OnLose()
    {
        var planningDistance = Mathf.Floor(_player.position.z - StartPlanning);
        var racingDistance = Mathf.Floor(StartPlanning - StartRace);

        _textResult.text = 
            $"{KilledEnemy}\n" +
            $"{racingDistance}\n" +
            $"{planningDistance}\n" +
            $"{Distance}";

        KilledEnemy = 0;
    }

    private void Update()
    {
        if (!_isActive) return;
        Distance = _player.position.z - StartRace;
    }
}