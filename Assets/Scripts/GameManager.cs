using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // A simpleton, everything can access this file
    public static GameManager gameManager { get; private set; }
    
    private int _currentScore;
    private int _currentExp;
    private int _levelUpExp = 100;
    
    [SerializeField] private ExperienceBar experienceBar;
    private UiScoreChanger _textScoreChanger;
    
    private UIScript _uiScript;
    
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
        _uiScript =  GetComponent<UIScript>();
        
        MaxExperiencePoints(_levelUpExp);
        
    }
    
    public void AddScore(int amount)
    {
        _currentScore += amount;
        _textScoreChanger.ChangeScoreText(_currentScore);
    }

    public int GetCurrentScore()
    {
        return _currentScore;
    }
    
    public void AddExperiencePoints(int amount)
    {
        
        _currentExp += amount;
        experienceBar.slider.value = _currentExp;
        
        if (_currentExp < _levelUpExp) return;
        
        LevelUp();
        
    }

    public void MaxExperiencePoints(int amount)
    {
        experienceBar.slider.maxValue = amount;
    }

    private void LevelUp()
    { 
        _levelUpExp += _levelUpExp;
        MaxExperiencePoints(_levelUpExp);
        _currentExp *= 0;
        AddExperiencePoints(0);
        
        
          // Pause game 
          // Play sound and ad a menu for choosing player uppgrade
        Debug.Log("Level Up");
    }
    
    public void GameOver()
    {
        _uiScript.DeathMenu();

        // Debug.Log("Game Over");

        // saves score 
        // Plays sad sounds, show death screen with current score and highscore, option to restart or quit to main menu/game

    }

    public void SaveScore()
    {
        
    }
    
    public void Restart()
    {
        
    }

    public void Quit()
    {
        
    }
    
}
