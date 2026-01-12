using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [SerializeField] private DialogueDatas _dialogueData;
    private DialogueRow _currentRow;
    private int _currentRowIndex;
    [SerializeField] private UiDialogueController _dialogueController;
    private int _conversationIndex = 0;
    [SerializeField] private bool _loopDialogues = true;

    public void Action(Pawn CurrentPawn)
    {
        _currentRowIndex = _conversationIndex;
        _currentRow = GetDialogueRow(CurrentPawn);
        _dialogueController.StartDialogue(this);
    }

    public DialogueRow GetDialogueRow(Pawn pawn)
    {
        DialogueRow row = _dialogueData.rows[_currentRowIndex];

        if (row.needGavel && !pawn.GetPlayerData()._hasGavel)
        {
            if (row.alternativeRowNumber != -1)
            {
                _currentRowIndex = row.alternativeRowNumber;
                return _dialogueData.rows[_currentRowIndex];
            }
        }

        return row;
    }

    public string GetDialogueText()
    {
        return _currentRow.longDialogue;
    }

    public string GetCharacterName()
    {
        return _currentRow.characterName;
    }

    public void GetNextRow(Pawn pawn)
    {
        if (_currentRow.nextRowNumber == -1)
        {
            _dialogueController.EndDialogue();
            AdvanceConversation();
            return;
        }

        _currentRowIndex = _currentRow.nextRowNumber;
        _currentRow = GetDialogueRow(pawn);
        _dialogueController.UpdateText();
    }

    private void AdvanceConversation()
    {
        _conversationIndex++;

        if (_conversationIndex >= _dialogueData.rows.Length)
        {
            if (_loopDialogues)
            {
                _conversationIndex = 0;
            }
            else
            {
                _conversationIndex = _dialogueData.rows.Length - 1;
            }
        }
    }

    public void ResetConversation()
    {
        _conversationIndex = 0;
    }
}

















