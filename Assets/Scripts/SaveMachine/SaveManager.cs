using System;
using System.IO;
using UnityEngine;
using Newtonsoft.Json;

public static class SaveManager
{
    private static readonly string saveFolder = Application.persistentDataPath + "/GameData";

    
    // Delete
    public static void Delete(string profileName)
    {
        if (!File.Exists($"{saveFolder}/{profileName}"))
        {
            throw new Exception($"Save Profile {profileName} does not exist");
        }
        Debug.Log($"Successfully deleted {profileName}");
        File.Delete($"{saveFolder}/{profileName}");
    }
    
    // Load
    public static SaveProfile<T> Load<T>(string profileName) where T : SavePlayerHighScore
    {
        if (!File.Exists($"{saveFolder}/{profileName}"))
        {
            throw new Exception($"Save Profile {profileName} does not exist");
        }
        
        var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
        
        Debug.Log($"Loading Profile {profileName}");
        
        return JsonConvert.DeserializeObject<SaveProfile<T>>(fileContents);
    }

    // Save
    public static void Save<T>(SaveProfile<T> save) where T : SavePlayerHighScore
    {
        // if (File.Exists($"{saveFolder}/{save.profileName}"))
        // {
        //     throw new Exception($"Save Profile {save.profileName} does  exist");
        // }
        var jsonString = JsonConvert.SerializeObject(save, Formatting.Indented, new JsonSerializerSettings{ReferenceLoopHandling = ReferenceLoopHandling.Ignore});
        
        // if add encrypt method
        
        
        if (!Directory.Exists(saveFolder))
        {
            Directory.CreateDirectory(saveFolder);
        }
        File.WriteAllText($"{saveFolder}/{save.profileName}", jsonString);
        Debug.Log($"Saved to {save.profileName}");
    }
    
}
