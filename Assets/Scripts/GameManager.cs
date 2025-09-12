using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{

    public int maxHealth;
    
    // A simpleton, everything can access this file
    public static GameManager gameManager { get; private set; }

    // public HealthUnit PlayerHealth = new HealthUnit(100, 100);
    // public HealthUnit PlayerHealth;
    
    private GameObject[] _enemies;

    [SerializeField] private float _ESpeed;
    [SerializeField] private GameObject Player;
    
    public int currentScore;
    public int currentExp;
    
    
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
        // GetEnemies();
        //
        // foreach (var enemy in _Enemies)
        // {
        //     Debug.Log(enemy.gameObject.name);
        // }
        
        
    }
    
    // void Update()
    // {
    //     GetEnemies();
    //     
    //     foreach (var enemy in _Enemies)
    //     {
    //
    //         float speedPlus = Random.Range(1, 3);
    //         enemy.transform.position = Vector3.MoveTowards(enemy.transform.position, Player.transform.position, _ESpeed * Time.deltaTime + speedPlus);
    //
    //         // Debug.Log(enemy.gameObject.name);
    //     }
    // }
    
    // void GetEnemies()
    // {
    //     _Enemies = GameObject.FindGameObjectsWithTag("Enemy");
    // }


    public void AddScore(int amount)
    {
        currentScore += amount;
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
