using UnityEngine;
using UnityEngine.Events;

public class EnemyLevels : MonoBehaviour
{
    private int _enemyLevel = 1;
    private EnemySpawner _enemySpawner;

    private EnemyBehaviour zombie;

    private void Start()
    {
        GameManager.Instance.onEnemyLevelUp?.AddListener( CheckEnemyLevel );

        _enemySpawner = GetComponent<EnemySpawner>();

        zombie = _enemySpawner.enemies[0].GetComponent<EnemyBehaviour>();
        

        CheckEnemyLevel();
    }


    private void CheckEnemyLevel()
    {
        Debug.Log($"Check enemy level {_enemyLevel}");
        switch (_enemyLevel)
        {
            case 1:
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(0, _enemySpawner.spawnDelay0));
                break;
            
            case 2:
                Debug.Log("Enemy Level 2");
                _enemySpawner.spawnDelay1 -= 0.2f;
                // zombie.runTimeStatSo.speed += 10;
                break;
            case 3:
                _enemySpawner.StartCoroutine(_enemySpawner.SpawnRoutine(1, _enemySpawner.spawnDelay1));
                break;



            default:
            {
                Debug.Log("Enemy max Level or ????minus level????");
                break;
            }
        }
        
        _enemyLevel++;
    }

    private void EnemyLevelUp()
    {
        _enemyLevel++;
    }
    
}
