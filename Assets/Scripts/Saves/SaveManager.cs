using System;
using System.Diagnostics;
using System.IO;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class SaveManager : MonoBehaviour
{
    private string saveFilePath;
    public Player player;
    public DoorOpen door;
    private String encryptionKey = "9O?_0_{ayzS$jjePKqR<(:<w9)]Jz&.,";

    void Start()
    {
        // Set the save file path
        saveFilePath = Path.Combine(Application.dataPath.Replace("/Assets", ""), "savefile.json");
        Debug.Log($"Save path: {saveFilePath}");
    }

    public void SaveGame(PlayerData playerData, DoorData doorData)
    {
        SaveData data = new SaveData
        {
            playerData = playerData,
            doorData = doorData
        };
        
        // // Convert the data to JSON format
        // string json = JsonUtility.ToJson(data, true); // Pretty print for readability
        //
        // // Write the JSON to a file
        // File.WriteAllText(saveFilePath, json);
        
        // Serialize critical data (encrypted)
        string criticalJson = JsonUtility.ToJson(data.playerData);
        string encryptedCriticalData = SaveEncryption.Encrypt(criticalJson, encryptionKey);

        // Serialize editable data (plain text)
        string editableJson = JsonUtility.ToJson(data.doorData);

        // Combine into a single file
        File.WriteAllText(saveFilePath, encryptedCriticalData + "|" + editableJson);
    }

    public void CreateSave()
    {
        SaveGame(player.SaveData(), door.SaveData());
    }
    
    public void LoadGame()
    {
        if (!File.Exists(saveFilePath))
        {
            Debug.LogWarning("Save file not found!");
            return;
        }
        string saveContent = File.ReadAllText(saveFilePath);
        string[] parts = saveContent.Split('|');
        if (parts.Length != 2) return;

        // Decrypt critical data
        string decryptedCriticalData = SaveEncryption.Decrypt(parts[0], encryptionKey);
        PlayerData playerData = JsonUtility.FromJson<PlayerData>(decryptedCriticalData);

        // Deserialize editable data
        DoorData doorData = JsonUtility.FromJson<DoorData>(parts[1]);
        
        player.LoadData(playerData);
        door.LoadData(doorData);
        
        // Combine into SaveData
        // return new SaveData { player = player, settings = settings };
        
        // if (File.Exists(saveFilePath))
        // {
        //     string json = File.ReadAllText(saveFilePath);
        //     SaveData gameData = JsonUtility.FromJson<SaveData>(json);
        //     Debug.Log("Game loaded!");
        //     player.LoadData(gameData.playerData);
        //     door.LoadData(gameData.doorData);
        // }
        // else
        // {
        //     Debug.LogWarning("Save file not found!");
        // }
    }
    
    public void OpenFileInTextEditor()
    {
        if (string.IsNullOrEmpty(saveFilePath))
        {
            Debug.LogError("File path is not set.");
            return;
        }

        if (!File.Exists(saveFilePath))
        {
            Debug.LogError($"File does not exist at path: {saveFilePath}");
            return;
        }

        // Use the default text editor to open the file
        Process.Start(new ProcessStartInfo
        {
            FileName = saveFilePath,
            UseShellExecute = true
        });
    }
}