using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // A simpleton, everything can access this file
    public static GameManager gameManager { get; private set; }
    
    public int currentScore;
    public int currentExp;
    
    private UiScoreChanger _textScoreChanger;
    
    void Awake()
    {
        // Playerhealth = new HealthUnit(100, 100);
        
        // if there are more game managers in the scene this game manager gets removed
        if (gameManager != null && gameManager != this)
        {
            Destroy(this);
        }
        else
        {
            gameManager = this;
        }
        
    }

    void Start()
    {
        _textScoreChanger = GetComponent<UiScoreChanger>();
    }
    
    
    public void AddScore(int amount)
    {
        currentScore += amount;
        Debug.Log("Current Score " +currentScore);
        _textScoreChanger.ChangeScoreText(currentScore);
    }

    public void AddExperiencePoints(int amount)
    {
        currentExp += amount;
    }
    
    
    public void GameOver()
    {
        
        // Debug.Log("Game Over");
        // saves score 
        // Plays sad sounds, show death screen with current score and highscore, option to restart or quit to main menu/game
    }
    
}
