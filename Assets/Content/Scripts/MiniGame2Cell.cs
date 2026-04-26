using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGame2Cell : MonoBehaviour, IActionnable
{
    private const string MiniGame2SceneName = "minigame2";

    /// <summary>Loads the MiniGame2 scene when the pawn lands on this cell.</summary>
    public void Action(Pawn currentPawn)
    {
        SceneManager.LoadScene(MiniGame2SceneName);
    }
}
