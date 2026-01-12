using UnityEngine;

public class GavelCell : MonoBehaviour, IActionnable
{
    [SerializeField] public UIObjectController _uiobject;
    public void Action(Pawn currentPawn)
    {
        PlayerDatas playerData = currentPawn.GetPlayerData();
        if (playerData != null)
        {
          playerData._hasGavel = true;
            _uiobject.HasGavel();
          
            
        }
    }
}
