using UnityEngine;
using UnityEngine.UI;

public class MiniGameIntroPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panelRoot;
    [SerializeField] private Button _startButton;
    [SerializeField] private MiniGameTimer _timer;
    [SerializeField] private MiniGamePlayerController _playerController;

    private PolicierController[] _policiers;

    private void Awake()
    {
        _policiers = FindObjectsByType<PolicierController>(FindObjectsSortMode.None);
        _playerController.Freeze();
        _panelRoot.SetActive(true);
        _startButton.onClick.AddListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        _panelRoot.SetActive(false);
        _playerController.Unfreeze();
        _timer.StartTimer();

        foreach (PolicierController policier in _policiers)
            policier.StartPatrol();
    }
}

