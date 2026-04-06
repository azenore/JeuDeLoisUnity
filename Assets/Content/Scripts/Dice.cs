using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Dice : MonoBehaviour
{
    [SerializeField] private Pawn _pawn;
    [SerializeField] private TMP_Text _diceResultText;
    [SerializeField] private Button _diceButton;
    [SerializeField] private UiDialogueController _dialogueController;

    private void Update()
    {
        if (_diceButton == null)
            return;

        bool dialogueActive = _dialogueController != null && _dialogueController.IsDialogueActive;
        _diceButton.interactable = !_pawn.IsMoving && !dialogueActive;
    }

    /// <summary>
    /// Rolls the dice and moves the pawn. Ignored if the pawn is already moving or a dialogue is displayed.
    /// </summary>
    public void RollTheDice()
    {
        bool dialogueActive = _dialogueController != null && _dialogueController.IsDialogueActive;

        if (_pawn.IsMoving || dialogueActive)
            return;

        int value = Random.Range(1, 7);

        if (_diceResultText != null)
            _diceResultText.text = $"Dice : {value}";

        Debug.Log($"Le dé a fait {value}");
        _pawn.TryMoving(value);
    }
}
