using UnityEngine;
using System.Collections;


public class Pawn : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private PlayerDatas _playerData;
    [SerializeField] private Dice _dice;

    public bool IsMoving { get; private set; }

    private void Start()
    {
        _playerData.InitializeIfNeeded();
        MoveToCell();
    }

    public void TryMoving(int value)
    {
        StartCoroutine(MoveStepByStep(value));
    }

    private IEnumerator MoveStepByStep(int steps)
    {
        IsMoving = true;

        for (int i = 0; i < steps; i++)
        {
            _playerData._cellNumber = _board.GetNextCellToMove(_playerData._cellNumber + 1);
            MoveToCell();
            yield return new WaitForSeconds(0.3f);
        }

        IsMoving = false;
        ActivateCell();
    }

    private void MoveToCell()
    {
            Transform NewPos = _board.GetCellByNumber(_playerData._cellNumber).transform;
            transform.position = NewPos.position;
            transform.rotation = NewPos.rotation;
    }

    private void ActivateCell()
    {
        Cell cell = _board.GetCellByNumber(_playerData._cellNumber);
        cell.Activate(this);
    }

    /// <summary>
    /// Returns the PlayerDatas ScriptableObject associated with this pawn.
    /// </summary>
    public PlayerDatas GetPlayerData()
    {
        return _playerData;
    }
}
