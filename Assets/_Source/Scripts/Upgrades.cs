using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Upgrades : MonoBehaviour
{
    [SerializeField] private int _id;

    [SerializeField] private ButtonBase _button;
    [SerializeField] private TextMeshProUGUI _textPrice;

    [SerializeField] private int[] _prices;
    [SerializeField] private Image[] _images;

    [SerializeField] private Sprite _enable, _disable;


    private void Start()
    {
        _button.OnClick.AddListener(Upgrade);
        UpdateUI();
    }

    private void Upgrade()
    {
        if (Game.Wallet.Spend(_prices[Game.Data.Saves.Upgrades[_id]]))
        {
            Game.Data.Saves.Upgrades[_id]++;
            Game.Data.SaveProgress();
            UpdateUI();
        }
        else Game.Audio.PlayClip(4);
    }

    private void UpdateUI()
    {
        for(int i = 0; i < _images.Length; i++)
        {
            _images[i].sprite = Game.Data.Saves.Upgrades[_id] > i ? _enable : _disable;
        }

        _textPrice.text = $"{_prices[Game.Data.Saves.Upgrades[_id]]}<sprite=0>";
    }
}