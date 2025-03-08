using TMPro;
using UnityEngine;

public class GameStatistic : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _textDistance;
    [SerializeField] private TextMeshProUGUI _textResult;
    [SerializeField] private TextMeshProUGUI _textRecord;
    [SerializeField] private TextMeshProUGUI _textReward;

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

        UpdateTextRecord();
    }

    private void Action_OnStart()
    {
        StartRace = _player.transform.position.z;
    }

    private void Action_OnLose()
    {
        var planningDistance = Mathf.FloorToInt(_player.position.z - StartPlanning);
        var racingDistance = Mathf.FloorToInt(StartPlanning - StartRace);
        var totalDistance = Mathf.FloorToInt(Distance);

        _textResult.text = 
            $"{KilledEnemy}\n" +
            $"{racingDistance}\n" +
            $"{planningDistance}\n" +
            $"{totalDistance}";

        var data = Game.Data.Saves;

        if (planningDistance > data.RecordPlanningDistance)
            data.RecordPlanningDistance = planningDistance;
        if (racingDistance > data.RecordRacingDistance)
            data.RecordRacingDistance = racingDistance;
        if (totalDistance > data.RecordTotalDistance)
            data.RecordTotalDistance = totalDistance;
        if (KilledEnemy > data.RecordKilledEnemy)
            data.RecordKilledEnemy = KilledEnemy;

        UpdateTextRecord();

        int ClaimedMoney = (KilledEnemy * 1000) + (racingDistance * 5) + planningDistance;

        _textReward.text = $"Reward {ClaimedMoney}<sprite=0>";

        Game.Wallet.Add(ClaimedMoney);

        Game.Data.SaveProgress();

        KilledEnemy = 0;
        _isActive = false;
    }

    private void UpdateTextRecord()
    {
        var data = Game.Data.Saves;

        var killed = data.RecordKilledEnemy == 0 ? "none" : $"{data.RecordKilledEnemy}";
        var racing = data.RecordRacingDistance == 0 ? "none" : $"{data.RecordRacingDistance}";
        var planning = data.RecordPlanningDistance == 0 ? "none" : $"{data.RecordPlanningDistance}";
        var total = data.RecordTotalDistance == 0 ? "none" : $"{data.RecordTotalDistance}";

        _textRecord.text =
            $"{killed}\n" +
            $"{racing}\n" +
            $"{planning}\n" +
            $"{total}\n";
    }

    private void Update()
    {
        if (!_isActive) return;
        Distance = _player.position.z - StartRace;
    }
}