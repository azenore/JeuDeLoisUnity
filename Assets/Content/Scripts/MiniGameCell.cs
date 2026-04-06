using UnityEngine;
using UnityEngine.SceneManagement;

public class MiniGameCell : MonoBehaviour, IActionnable
{
    private const string MiniGameSceneName = "MiniGame";

    public void Action(Pawn currentPawn)
    {
        SceneManager.LoadScene(MiniGameSceneName);
    }
}
