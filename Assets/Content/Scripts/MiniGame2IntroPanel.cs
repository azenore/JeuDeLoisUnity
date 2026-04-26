using UnityEngine;
using UnityEngine.UI;

public class MiniGame2IntroPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panelRoot;
    [SerializeField] private Button _startButton;

    private void Awake()
    {
        _panelRoot.SetActive(true);
        _startButton.onClick.AddListener(OnStartClicked);
    }

    /// <summary>Hides the intro panel and starts the mini-game.</summary>
    private void OnStartClicked()
    {
        _panelRoot.SetActive(false);
    }
}
