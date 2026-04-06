using TMPro;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [SerializeField] private Pawn _pawn;
    [SerializeField] private TMP_Text _diceResultText;

    public void RollTheDice()
    {
        int value = Random.Range(1, 7);

        if (_diceResultText != null)
            _diceResultText.text = $"Dice : {value}";

        Debug.Log($"Le dé a fait {value}");
       _pawn.TryMoving(value);
    }
}
