using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath;
    public Player player;
    public DoorOpen door;

    void Start()
    {
        // Set the save file path
        saveFilePath = Path.Combine(Application.persistentDataPath, "savefile.json");
        Debug.Log($"Save path: {saveFilePath}");
    }

    public void SaveGame(PlayerData playerData, DoorData doorData)
    {
        SaveData data = new SaveData
        {
            playerData = playerData,
            doorData = doorData
        };
        
        // Convert the data to JSON format
        string json = JsonUtility.ToJson(data, true); // Pretty print for readability

        // Write the JSON to a file
        File.WriteAllText(saveFilePath, json);
    }

    public void CreateSave()
    {
        SaveGame(player.SaveData(), door.SaveData());
    }
    
    public void LoadGame()
    {
        if (File.Exists(saveFilePath))
        {
            string json = File.ReadAllText(saveFilePath);
            SaveData gameData = JsonUtility.FromJson<SaveData>(json);
            Debug.Log("Game loaded!");
            player.LoadData(gameData.playerData);
            door.LoadData(gameData.doorData);
        }
        else
        {
            Debug.LogWarning("Save file not found!");
        }
    }

}