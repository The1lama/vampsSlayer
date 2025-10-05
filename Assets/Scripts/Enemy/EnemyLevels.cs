using UnityEngine;
using UnityEngine.Events;

public class EnemyLevels : MonoBehaviour
{
    private EnemySpawner _enemySpawner;

    public GameObject[] enemies;
    
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
        int enemyLevel = GameManager.Instance.EnemyLevels;
        Debug.Log($"Check enemy level {enemyLevel}");
        
        switch (enemyLevel)
        {
            case 1:
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(enemies[0]));
                break;
            
            case 2:
                Debug.Log("Enemy Level 2");
                break;
            case 3:
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(enemies[1]));
                break;
            
            case 4:
                Debug.Log("Enemy Level 3");
                break;
            
            case 5:
                Debug.Log("Enemy Level 4");
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(enemies[2]));
                break;


            default:
            {
                Debug.Log("Enemy max Level or ????minus level????");
                break;
            }
        }
    }
    
}
