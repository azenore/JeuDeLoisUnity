using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameCell : MonoBehaviour, IActionnable
{
    private const string MiniGameSceneName = "MiniGame";

    /// <summary>
    /// Triggered when the pawn lands on this cell. Loads the MiniGame scene.
    /// </summary>
    public void Action(Pawn currentPawn)
    {
        SceneManager.LoadScene(MiniGameSceneName);
    }
}
