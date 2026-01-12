using TMPro;
using UnityEngine;

public class UiDialogueController : MonoBehaviour
{

    [SerializeField] private DialogueComponent _dialogueComponent;
    [SerializeField] private GameObject _dialoguePanel;
    [SerializeField] private TMP_Text _characterNameText;
    [SerializeField] private TMP_Text _dialogueText;
    private Pawn _currentPawn;
    private void Start()
    {
        _dialoguePanel.SetActive(false);
    }
    public void StartDialogue(DialogueComponent dialogueComponent)
    {
       _dialogueComponent = dialogueComponent;
        UpdateText();
        _dialoguePanel.SetActive(true);
    }

    public void SetPawn(Pawn pawn)
    {
        _currentPawn = pawn;
    }

    public void ChangeRow()
    {
        _dialogueComponent.GetNextRow();
    }

    public void UpdateText()
    {
        _characterNameText.text = _dialogueComponent.GetCharacterName();
        _dialogueText.text = _dialogueComponent.GetDialogueText();
    }

    public void EndDialogue()
    {
        _dialoguePanel.SetActive(false);
    }

}
