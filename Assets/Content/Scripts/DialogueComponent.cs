using UnityEngine;

public class DialogueComponent : MonoBehaviour, IActionnable
{
    [SerializeField] private DialogueDatas _dialogueData;
    private DialogueRow _currentRow;
    private int _currentRowIndex;
    [SerializeField] private UiDialogueController _dialogueController;

    public void Action(Pawn _currentPawn)
    {
        _currentRow = GetDialogueRow(_currentPawn);
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



   // public DialogueRow GetDialogueRow()
  //  {
   //     return _dialogueData.rows[_currentRowIndex];
   // }

    public string  GetDialogueText()
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
            return;
        } 
        _currentRowIndex = _currentRow.nextRowNumber;
        _currentRow = GetDialogueRow(pawn);
        _dialogueController.UpdateText();


    }
}
