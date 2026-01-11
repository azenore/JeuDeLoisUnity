using UnityEngine;

public class Pawn : MonoBehaviour
{
    [SerializeField] private Board _board;
    [SerializeField] private PlayerDatas _playerData;
    [SerializeField] private Dice _dice;
    private void Start()
    {
        MoveToCell();
       // ActivateCell();// penser à enlever
    }

    public void TryMoving(int value)
    {
        //int NextCellToGo = _board.GetNextCellToMove(_playerData._cellNumber + value);//
        _playerData._cellNumber = _board.GetNextCellToMove(_playerData._cellNumber + value);
        MoveToCell();
        ActivateCell();
    }
    private void MoveToCell()
    {
            Transform NewPos = _board.GetCellByNumber(_playerData._cellNumber).transform;  // TODO : get cell number to do
            transform.position = NewPos.position;
            transform.rotation = NewPos.rotation;
    }

    private void ActivateCell()
    {
        Cell cell = _board.GetCellByNumber(_playerData._cellNumber);
        cell.Activate(this);
    }
        

    //private void MoveToCell()
    //{
    //    Transform NewPos = _board.GetCellByNumber(_playerData._cellNumber).transform;  // TODO : get cell number to do
    //    transform.position = NewPos.position;
    //    transform.rotation = NewPos.rotation;
    //}



}
