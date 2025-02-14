using DG.Tweening;
using UnityEngine;

public class ButtonSettings : MonoBehaviour
{
    private bool _isShow;
    private Sequence _sequence;

    private void Start()
    {
        Game.Action.OnEnter += Action_OnEnter;
    }

    private void Action_OnEnter()
    {
        throw new System.NotImplementedException();
    }

    private void Hide()
    {

    }

    private void Show()
    {

    }
}