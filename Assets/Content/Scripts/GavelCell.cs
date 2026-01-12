using UnityEngine;

public class GavelCell : MonoBehaviour, IActionnable
{
    public void Action(Pawn currentPawn)
    {
        PlayerDatas playerData = currentPawn.GetPlayerData();
        if (playerData != null)
        {
          playerData._hasGavel = true;
          
            
        }
    }
}
