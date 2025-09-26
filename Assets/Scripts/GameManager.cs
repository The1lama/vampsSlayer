using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    // A simpleton, everything can access this file
    public static GameManager Instance { get; private set; }
    
    private int _currentScore;
    private int _currentExp;
    [SerializeField] private int levelUpExp = 100;
    
    [SerializeField] private ExperienceBar experienceBar;
    private UiScoreChanger _textScoreChanger;
    
    private UiStateManager _uiStateManager;

    public UnityEvent onDeath;
    public UnityEvent onLevelUp;
    
    void Awake()
    {
        // if there are more game managers in the scene this game manager gets removed
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        
        onDeath?.AddListener(GameOver);
        onLevelUp?.AddListener(LevelUp);
        
    }

    void Start()
    {
       _textScoreChanger = GetComponent<UiScoreChanger>();
       _uiStateManager = GetComponent<UiStateManager>();
        
       MaxExperiencePoints(levelUpExp);
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
        
        if (_currentExp <= levelUpExp) return;
        
        onLevelUp?.Invoke();    // Sends out call for onLevelUp
    }

    private void MaxExperiencePoints(int amount)
    {
        experienceBar.slider.maxValue = amount;
    }

    private void LevelUp()
    { 
        levelUpExp += levelUpExp/2;
        MaxExperiencePoints(levelUpExp);
        _currentExp = 0;
        experienceBar.slider.value = 0;
        
          // Pause game 
          // Play sound and ad a menu for choosing player uppgrade; Sound manager who lisens to onDeath call and then plays sound? or a method in this file to play sound?
    }
    
    private void GameOver()
    {
        SaveHighScore();
        
        // Plays sad sounds; Sound manager who lisens to onDeath call and then plays sound? or a method in this file to play sound?
    }

    public void SaveHighScore()
    {
        if (_currentScore > SaveManager.Load<SavePlayerHighScore>("playerHighScore").saveData.HighScore)
        {
            var playerScoreSave = new SavePlayerHighScore{ HighScore = _currentScore };
            var saveProfile = new SaveProfile<SavePlayerHighScore>("playerHighScore", playerScoreSave);
            SaveManager.Save(saveProfile);
        }
        else
        {
            Debug.Log("No new high score");
        }
    }
    
    
}
