using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameManager : MonoBehaviour
{
    private const string DevMapSceneName = "Dev_Map";
    private const int WinMoneyReward = 100;
    private const int CaughtMoneyPenalty = 150;
    private const float GameOverDelay = 1.5f;

    /// <summary>PlayerPrefs key used to communicate the mini-game result back to the Dev Map.</summary>
    public const string MiniGameResultKey = "MiniGameResult";

    /// <summary>Stored in PlayerPrefs when the player wins the mini-game.</summary>
    public const int ResultWin = 1;

    /// <summary>Stored in PlayerPrefs when the player loses the mini-game.</summary>
    public const int ResultLose = -1;

    [SerializeField] private PlayerDatas _playerData;
    [SerializeField] private MiniGameTimer _timer;
    [SerializeField] private MiniGamePlayerController _playerController;

    private bool _isCaught = false;
    private bool _gameOver = false;

    /// <summary>Called when the player gets caught by a policier.</summary>
    public void OnPlayerCaught()
    {
        if (_gameOver)
            return;

        _gameOver = true;
        _isCaught = true;

        _playerController.Freeze();
        _timer.StopTimer();
        _playerData._money -= CaughtMoneyPenalty;

        PlayerPrefs.SetInt(MiniGameResultKey, ResultLose);
        PlayerPrefs.Save();

        Invoke(nameof(LoadDevMap), GameOverDelay);
    }

    /// <summary>Called by MiniGameTimer when time runs out.</summary>
    public void OnTimeUp()
    {
        if (_gameOver)
            return;

        _gameOver = true;

        if (!_isCaught)
        {
            _playerData._money += WinMoneyReward;
            PlayerPrefs.SetInt(MiniGameResultKey, ResultWin);
        }
        else
        {
            PlayerPrefs.SetInt(MiniGameResultKey, ResultLose);
        }

        PlayerPrefs.Save();
        LoadDevMap();
    }

    private void LoadDevMap()
    {
        SceneManager.LoadScene(DevMapSceneName);
    }
}
