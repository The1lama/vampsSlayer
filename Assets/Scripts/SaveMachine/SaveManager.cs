using System;
using System.IO;
using UnityEngine;
using NewtonSoft.Json;

public static class SaveManager
{
    private static readonly string saveFolder = Application.persistentDataPath + "/GameData";

    public static SaveProfile<T> Load<T>(string profileName) where T : SaveHighScoreData
    {
        if (!File.Exists($"{saveFolder}/{profileName}"))
        {
            throw new Exception($"Save Profile {profileName} does not exist");
        }
        
        var fileContents = File.ReadAllText($"{saveFolder}/{profileName}");
        
        Debug.Log($"Loading Profile {profileName}");
        
        return JsonConvert.DeserializeObject<SaveProfile<T>>(fileContents);
    }
}
