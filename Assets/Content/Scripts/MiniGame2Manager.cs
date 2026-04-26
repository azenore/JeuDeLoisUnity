using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>Manages the MiniGame2 win condition: click all 6 stop signs before the timer runs out.</summary>
public class MiniGame2Manager : MonoBehaviour
{
    private const string BoardSceneName = "Dev_Map";
    private const int TotalSigns = 6;
    private const int WinReward = 150;

    public static MiniGame2Manager Instance { get; private set; }

    [SerializeField] private PlayerDatas _playerDatas;
    [SerializeField] private Camera _camera;

    private int _clickedCount = 0;
    private bool _isActive = false;

    private void Awake()
    {
        Instance = this;
    }

    private void OnDestroy()
    {
        if (Instance == this)
            Instance = null;
    }

    private void Update()
    {
        if (!_isActive)
            return;

        if (!Input.GetMouseButtonDown(0))
            return;

        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            StopSign sign = hit.collider.GetComponentInParent<StopSign>();
            if (sign != null)
                sign.OnClicked();
        }
    }

    /// <summary>Enables click detection — called by MiniGame2IntroPanel when the game starts.</summary>
    public void StartGame()
    {
        _isActive = true;
    }

    /// <summary>Called by each StopSign when clicked. Triggers win if all signs are clicked.</summary>
    public void RegisterSignClicked()
    {
        _clickedCount++;

        if (_clickedCount >= TotalSigns)
            OnWin();
    }

    private void OnWin()
    {
        _playerDatas._money += WinReward;
        SceneManager.LoadScene(BoardSceneName);
    }
}
