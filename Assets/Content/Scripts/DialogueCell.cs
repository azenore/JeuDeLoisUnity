using UnityEngine;

public class DialogueCell : Cell
{
    [SerializeField] private DialogueComponent _dialogueComponent;
    [SerializeField] private UiDialogueController _dialogueController;

    public override void Activate(Pawn CurrentPawn)
    {
        _dialogueController.SetPawn(CurrentPawn);
        _dialogueComponent.Action(CurrentPawn);
    }
}
