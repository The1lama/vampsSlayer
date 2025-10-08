using UnityEngine;

public class EnemyLevels : MonoBehaviour
{
    private EnemySpawner _enemySpawner;
    public GameObject[] enemies;
    public int enemyLevelUpTime;
    public bool isSpawning = true;
    private int _currentEnemySpawn = 0;
    
    private void Start()
    {
        if (enemies == null || enemies.Length == 0)
        {
            Debug.LogWarning($"No enemies assigned, in <color=yellow>{name}</color>");
        }
        
        GameManager.Instance.onEnemyLevelUp?.AddListener( CheckEnemyLevel );
        _enemySpawner = GetComponent<EnemySpawner>();
        
        CheckEnemyLevel();
    }


    private void CheckEnemyLevel()
    {
        if (!isSpawning) return;
        
        var enemyLevel = GameManager.Instance.EnemyLevels;
        Debug.Log($"Check enemy level <Color=green>{enemyLevel}</Color>");

        
        if ((enemyLevel % 2 != 0) && (_currentEnemySpawn < enemies.Length))
        {
            _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(enemies[_currentEnemySpawn]));
            _currentEnemySpawn++;
        }
        else if ((enemyLevel % 2 != 0) && (_currentEnemySpawn > enemies.Length))
        {
            Debug.LogWarning("Max Enemy diffrent types has spawned");
            Debug.Log($"Enemy level: <Color=yellow>{enemyLevel}</Color>");
        }
        else
        {
            Debug.Log($"Enemy level: <Color=yellow>{enemyLevel}</Color>");
        }
    }
    
}
