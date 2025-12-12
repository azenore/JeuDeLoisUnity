using UnityEngine;
[System.Serializable]
public struct DialogueRow
{
    public int rowNumber;
    public string characterName;
    public string longDialogue;
    public int nextRowNumber;

}

[CreateAssetMenu(fileName = "DialogueDatas", menuName = "Scriptable Objects/DialogueDatas")]
public class DialogueDatas : ScriptableObject
{
    public DialogueRow[] rows;
}
