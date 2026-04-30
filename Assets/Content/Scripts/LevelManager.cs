using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField]   GameDatas gameDatas;

    public void ContinueGame()
    {
        GetComponent<SaveManager>().LoadGame();
        if (gameDatas.datas.IsPlayerInMinigame)
        {
            SceneManager.LoadScene(gameDatas.datas.MinigameNumbers);
        }
        else
        {
            SceneManager.LoadScene(2);
        }
    }
}
