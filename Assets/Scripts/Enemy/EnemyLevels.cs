using UnityEngine;
using UnityEngine.Events;

public class EnemyLevels : MonoBehaviour
{
    private int enemyLevel;
    private EnemySpawner _enemySpawner;


    private void Start()
    {
        GameManager.Instance.onEnemyLevelUp?.AddListener( CheckEnemyLevel );
        enemyLevel = GameManager.Instance.enemyLevels;
        _enemySpawner = GetComponent<EnemySpawner>();
        
        CheckEnemyLevel();
    }


    private void CheckEnemyLevel()
    {
        Debug.Log($"Check enemy level {enemyLevel}");
        switch (enemyLevel)
        {
            case 1:
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(0, enemyLevel));
                break;
            
            case 2:
                Debug.Log("Enemy Level 2");
                enemyLevel++;
                break;
            case 3:
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(1,  enemyLevel));
                break;
            
            case 4:
                Debug.Log("Enemy Level 3");
                enemyLevel++;
                break;
            
            case 5:
                Debug.Log("Enemy Level 4");
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(2, enemyLevel));
                break;


            default:
            {
                Debug.Log("Enemy max Level or ????minus level????");
                break;
            }
        }
        
        enemyLevel++;
    }
    
}
