using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [SerializeField] private DialogueDatas _dialogueData;
    private DialogueRow _currentRow;
    private int _currentRowIndex;
    [SerializeField] private UiDialogueController _dialogueController;

    public void Action(Pawn CurrentPawn)
    {
        _currentRow = GetDialogueRow();
        _dialogueController.StartDialogue(this);
    }


    public DialogueRow GetDialogueRow()
    {
        return _dialogueData.rows[_currentRowIndex];
    }

    public string  GetDialogueText()
    {
        return _currentRow.longDialogue;
    }

    public string GetCharacterName()
    {
        return _currentRow.characterName;
    }

    public void GetNextRow()
    {
        if (_currentRow.nextRowNumber == -1)
        {
            _dialogueController.EndDialogue();
            return;
        }
        _currentRowIndex = _currentRow.nextRowNumber;
        _currentRow = GetDialogueRow();
        _dialogueController.UpdateText();
    }
}
