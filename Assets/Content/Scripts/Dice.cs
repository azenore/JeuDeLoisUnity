using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] private Pawn _pawn;
    [SerializeField] private TMP_Text _diceResultText;
    [SerializeField] private Button _diceButton;
    [SerializeField] private UiDialogueController _dialogueController;
    [SerializeField] private GameIntroPanel _introPanel;

    private void Update()
    {
        if (_diceButton == null)
            return;

        bool dialogueActive = _dialogueController != null && _dialogueController.IsDialogueActive;
        bool introOpen = _introPanel != null && _introPanel.IsOpen;
        _diceButton.interactable = !_pawn.IsMoving && !dialogueActive && !introOpen;
    }

    public void RollTheDice()
    {
        bool dialogueActive = _dialogueController != null && _dialogueController.IsDialogueActive;
        bool introOpen = _introPanel != null && _introPanel.IsOpen;

        if (_pawn.IsMoving || dialogueActive || introOpen)
            return;

        int value = Random.Range(1, 7);

        if (_diceResultText != null)
            _diceResultText.text = $"Dice : {value}";

        Debug.Log($"Le dé a fait {value}");
        _pawn.TryMoving(value);
    }
}
