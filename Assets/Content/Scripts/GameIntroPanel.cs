using UnityEngine;
using UnityEngine.UI;

public class GameIntroPanel : MonoBehaviour
{
    private const string HasSeenIntroPrefKey = "GameIntroSeen";

    [SerializeField] private GameObject _panelRoot;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _diceButton;

    public bool IsOpen { get; private set; } = false;

    private void Awake()
    {
        bool hasSeenIntro = PlayerPrefs.GetInt(HasSeenIntroPrefKey, 0) == 1;

        if (hasSeenIntro)
        {
            _panelRoot.SetActive(false);
            _diceButton.interactable = true;
        }
        else
        {
            IsOpen = true;
            _panelRoot.SetActive(true);
            _diceButton.interactable = false;
            _startButton.onClick.AddListener(OnStartClicked);
        }
    }

    private void OnStartClicked()
    {
        PlayerPrefs.SetInt(HasSeenIntroPrefKey, 1);
        PlayerPrefs.Save();

        IsOpen = false;
        _panelRoot.SetActive(false);
        _diceButton.interactable = true;
    }
}
