using TMPro;
using UnityEngine;

public class UiMoneyController : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private PlayerDatas _playerData;
    [SerializeField] private GameObject _victoryScreen;
    [SerializeField] private GameObject _defeatScreen;

    private void Start()
    {
        UpdateMoneyDisplay();
        _victoryScreen.SetActive(false);
        _defeatScreen.SetActive(false);
    }

    public void UpdateMoneyDisplay()
    {
        if (_playerData._money >= 650)
        {
            _victoryScreen.SetActive(true);
        }

        if (_playerData._money < 0)
        {
            _defeatScreen.SetActive(true);
        }

        if (_playerData != null && _moneyText != null)
        {
            _moneyText.text = $"Money: {_playerData._money}";
        }
    }
}
