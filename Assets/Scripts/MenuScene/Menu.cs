using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textHighScore;
    
    
    private void Start()
    {
        var highScore = SaveManager.Load<SavePlayerHighScore>("playerHighScore").saveData.HighScore;
        textHighScore.text = string.Format("High Score\n{0}", highScore.ToString());
    }

    public void OnPlayButton()
    {
        SceneManager.LoadScene(SceneNames.GameScene);
    }

    public void OnQuitButton()
    {
        Debug.Log("Quit");
        Application.Quit();
    }
}

public static class SceneNames
{
    public const string MainMenu = "StartMenu";
    public const string GameScene = "GameScene";
}