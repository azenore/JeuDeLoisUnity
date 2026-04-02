using System.IO;
using UnityEngine;


public struct GameDatasStruc
{
    public int PlayerCellNumber;
    public bool IsPlayerInMinigame;
    public int MinigameNumbers;
}
public class SaveController
{
   public void SaveGameData(GameDatasStruc gameDatas,string filename)
    {
        string data = JsonUtility.ToJson(gameDatas);   
        string path = Application.dataPath + "/" + filename;
        File.WriteAllText(path, data);
    }

    public GameDatasStruc LoadGameData(string filename)
    {
        GameDatasStruc playerDatas = new GameDatasStruc();

        string path = Application.persistentDataPath + "/" + filename;

        if (File.Exists(path))
        {
            string data = File.ReadAllText(path);
            playerDatas = JsonUtility.FromJson<GameDatasStruc>(data);
        }
        else
        {
            SaveGameData(playerDatas, filename);
        }

        return playerDatas;
    }
}
