using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    [SerializeField] private RectTransform _hand;
    [SerializeField] private GameObject _sliceImage;

    private Tween _tween;

    private void Start()
    {
        Game.Action.OnStart += Action_OnStart;
    }

    private void Action_OnStart()
    {
        if (Game.Data.Saves.IsTutorialComplated) return;
        StartCoroutine(Tutorial1());
    }

    private IEnumerator Tutorial1()
    {
        yield return new WaitForSeconds(2f);
        Time.timeScale = 0.1f;
        _sliceImage.SetActive(true);

        _tween = _hand.DOScale(0.6f, 0.1f).SetLoops(-1, LoopType.Yoyo);

        yield return new WaitWhile(() => Game.Locator.Input.Horizontal == 0);
        _sliceImage.SetActive(false);

        _tween?.Kill();
        Time.timeScale = 1f;

        Game.Data.Saves.IsTutorialComplated = true;
        Game.Data.SaveProgress();
    }
}