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
        _victoryScreen.SetActive(false);
        _defeatScreen.SetActive(false);

        // Si on revient du mini-jeu, on consomme le résultat et on met à jour l'UI
        if (PlayerPrefs.HasKey(MiniGameManager.MiniGameResultKey))
        {
            PlayerPrefs.DeleteKey(MiniGameManager.MiniGameResultKey);
        }

        UpdateMoneyDisplay();
    }

    /// <summary>Rafraîchit le texte d'argent et vérifie les conditions de victoire/défaite.</summary>
    public void UpdateMoneyDisplay()
    {
        if (_playerData != null && _moneyText != null)
        {
            _moneyText.text = $"Money: {_playerData._money}";
        }

        if (_playerData._money >= 650)
        {
            _victoryScreen.SetActive(true);
        }

        if (_playerData._money < 0)
        {
            _defeatScreen.SetActive(true);
        }
    }
}
