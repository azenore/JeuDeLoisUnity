using UnityEngine;
using UnityEngine.UI;

public class GameIntroPanel : MonoBehaviour
{
    [SerializeField] private GameObject _panelRoot;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _diceButton;

    public bool IsOpen { get; private set; } = true;

    private void Awake()
    {
        _panelRoot.SetActive(true);
        _diceButton.interactable = false;
        _startButton.onClick.AddListener(OnStartClicked);
    }

    private void OnStartClicked()
    {
        IsOpen = false;
        _panelRoot.SetActive(false);
        _diceButton.interactable = true;
    }
}
