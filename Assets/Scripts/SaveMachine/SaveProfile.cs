using System;
using UnityEngine;

[Serializable]
public sealed class SaveProfile<T> where T : SaveHighScoreData
{
    public string name;
    public T saveData;
    
    private SaveProfile() { }
    
    public SaveProfile(string name, T saveData)
    {
        this.name = name;
        this.saveData = saveData;
    }
    
}


public abstract record SaveHighScoreProfileData { }

public record SaveHighScoreData : SaveHighScoreProfileData
{
    public int HighScore;
}

