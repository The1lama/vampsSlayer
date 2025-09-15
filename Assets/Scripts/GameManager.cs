using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // A simpleton, everything can access this file
    public static GameManager gameManager { get; private set; }
    
    public int currentScore;
    public int currentExp;
    public int levelUpExp = 100;
    
    [SerializeField] private ExperienceBar experienceBar;
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
        
        MaxExperiencePoints(levelUpExp);
        
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
        experienceBar.slider.value = currentExp;

        
        if (currentExp < levelUpExp) return;
        
        LevelUp();
        
    }

    public void MaxExperiencePoints(int amount)
    {
        experienceBar.slider.maxValue = amount;
    }

    private void LevelUp()
    { 
        levelUpExp += levelUpExp;
        MaxExperiencePoints(levelUpExp);
        currentExp *= 0;
        AddExperiencePoints(0);
        
        
          // Pause game 
          // Play sound and ad a menu for choosing player uppgrade
        Debug.Log("Level Up");
    }
    
    public void GameOver()
    {
        
        // Debug.Log("Game Over");
        // saves score 
        // Plays sad sounds, show death screen with current score and highscore, option to restart or quit to main menu/game
    }
    
}
