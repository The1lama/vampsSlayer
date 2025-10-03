using UnityEngine;
using UnityEngine.Events;

public class EnemyLevels : MonoBehaviour
{
    public int enemyLevel { get; private set; } = 1;
    private EnemySpawner _enemySpawner;


    private void Start()
    {
        GameManager.Instance.onEnemyLevelUp?.AddListener( CheckEnemyLevel );

        _enemySpawner = GetComponent<EnemySpawner>();
        
        CheckEnemyLevel();
    }


    private void CheckEnemyLevel()
    {
        Debug.Log($"Check enemy level {enemyLevel}");
        switch (enemyLevel)
        {
            case 1:
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(0, _enemySpawner.spawnDelay0, _enemySpawner.zombieLevel));
                break;
            
            case 2:
                Debug.Log("Enemy Level 2");
                _enemySpawner.spawnDelay1 -= 0.2f;
                _enemySpawner.zombieLevel += 2;
                break;
            case 3:
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(1, _enemySpawner.spawnDelay1, _enemySpawner.skeletonLevel));
                break;
            
            case 4:
                Debug.Log("Enemy Level 3");
                _enemySpawner.spawnDelay2 -= 0.2f;
                _enemySpawner.skeletonLevel += 2;
                break;
            
            case 5:
                Debug.Log("Enemy Level 4");
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(2, _enemySpawner.spawnDelay1, _enemySpawner.bigGuyLevel));
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
