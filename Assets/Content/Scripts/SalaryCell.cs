using UnityEngine;

public class SalaryCell : MonoBehaviour, IActionnable
{
    [SerializeField] private int _salaryAmount = 100;

    public void Action(Pawn currentPawn)
    {
        PlayerDatas playerData = currentPawn.GetPlayerData();

        if (playerData != null)
        {
            playerData._money += _salaryAmount;

            UiMoneyController uiMoney = FindFirstObjectByType<UiMoneyController>();
            if (uiMoney != null)
            {
                uiMoney.UpdateMoneyDisplay();
            }
        }
    }
}

