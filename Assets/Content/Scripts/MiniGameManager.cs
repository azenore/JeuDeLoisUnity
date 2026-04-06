using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    private const string DevMapSceneName = "Dev_Map";
    private const int WinMoneyReward = 100;
    private const int CaughtMoneyPenalty = 150;
    private const float GameOverDelay = 1.5f;

    [SerializeField] private PlayerDatas _playerData;
    [SerializeField] private MiniGameTimer _timer;
    [SerializeField] private MiniGamePlayerController _playerController;

    private bool _isCaught = false;
    private bool _gameOver = false;

    public void OnPlayerCaught()
    {
        if (_gameOver)
            return;

        _gameOver = true;
        _isCaught = true;

        _playerController.Freeze();
        _timer.StopTimer();
        _playerData._money -= CaughtMoneyPenalty;

        Invoke(nameof(LoadDevMap), GameOverDelay);
    }

    public void OnTimeUp()
    {
        if (_gameOver)
            return;

        _gameOver = true;

        if (!_isCaught)
            _playerData._money += WinMoneyReward;

        LoadDevMap();
    }

    private void LoadDevMap()
    {
        SceneManager.LoadScene(DevMapSceneName);
    }
}
