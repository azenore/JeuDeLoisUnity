using UnityEngine;

public class Cell : MonoBehaviour, ICellActivable
{
    public virtual void Activate(Pawn CurrentPawn)
    {
        if(GetComponent<IActionnable>() != null)
        {
            GetComponent<IActionnable>().Action(CurrentPawn);
        }
    }
}
