using TMPro;
using UnityEngine;

public class UiMoneyController : MonoBehaviour
{
    [SerializeField] private TMP_Text _moneyText;
    [SerializeField] private PlayerDatas _playerData;

    private void Start()
    {
        UpdateMoneyDisplay();
    }

    public void UpdateMoneyDisplay()
    {
        if (_playerData != null && _moneyText != null)
        {
            _moneyText.text = $"Money: {_playerData._money}";
        }
    }
}
