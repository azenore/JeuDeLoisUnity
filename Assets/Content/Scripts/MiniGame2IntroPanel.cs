using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MiniGame2IntroPanel : MonoBehaviour
{
    private const string BoardSceneName = "Dev_Map";
    private const int TimerPenalty = 80;

    [SerializeField] private GameObject _panelRoot;
    [SerializeField] private Button _startButton;
    [SerializeField] private MiniGameTimer _timer;
    [SerializeField] private PlayerDatas _playerDatas;

    private void Awake()
    {
        _panelRoot.SetActive(true);
        _startButton.onClick.AddListener(OnStartClicked);
        _timer.OnTimeUp.AddListener(OnTimeUp);
    }

    /// <summary>Hides the intro panel and starts the countdown timer.</summary>
    private void OnStartClicked()
    {
        Debug.Log("play");
        _timer.StartTimer();
        MiniGame2Manager.Instance.StartGame();
        _panelRoot.SetActive(false);
    }

    /// <summary>Called when the timer reaches zero — deducts gold and returns to the board scene.</summary>
    private void OnTimeUp()
    {
        _playerDatas._money -= TimerPenalty;
        SceneManager.LoadScene(BoardSceneName);
    }
}
