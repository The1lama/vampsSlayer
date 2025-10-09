using System.Collections;
using UnityEngine;
using uPools;

public class StartMenuScreenEnemyMove : MonoBehaviour
{
    [SerializeField] private GameObject[] SpawnPointHolder;

    [SerializeField] private GameObject[] EnemyTypes;

    [SerializeField] private float SpawnRateMin, SpawnRateMax, DieTime;
    
    private int _holderLenght;
    private int _enemyLenght;
    

    private void Start()
    {
        if ((EnemyTypes != null || SpawnPointHolder !=  null)) 
        { 
            Debug.Log("There are objects in list");
            _holderLenght = SpawnPointHolder.Length; 
            _enemyLenght = EnemyTypes.Length;
        
            StartCoroutine(SpawnTimer());
        }
        else
        {
            Debug.LogError($"<Color=Red>You need to assign {SpawnPointHolder} Spawn Point Holders and/or Enemy prefabs{EnemyTypes}</Color>");
        }
        
        if (!(SpawnRateMin != 0 || (SpawnRateMax != 0 && SpawnRateMin < SpawnRateMax)))
        {
            Debug.LogWarning("You should add a MIN and MAX spawn rate value\nOr have MAX value bigger than MIN value");
        }
    }

    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            SpawnEnemiesAtRandom();
                    
            var spawnTime = Random.Range(SpawnRateMin, SpawnRateMax);
            Debug.Log($"Spawn time: {spawnTime}");
            yield return new WaitForSeconds(spawnTime);
        }
    }
    
    private void SpawnEnemiesAtRandom()
    {
        
        var randomHolderIndex = Random.Range(0, _holderLenght);
        var randomEnemyIndex = Random.Range(0, _enemyLenght);
        
        var spawnPointObject = SpawnPointHolder[randomHolderIndex];
        var enemyPrefab = EnemyTypes[randomEnemyIndex];
        
        
        var enemy = ObjectPoolManager.SpawnObject(enemyPrefab, spawnPointObject.transform.position, Quaternion.identity);
        enemy.GetComponent<EnemyBehaviour>().Initialize(1);
    }
}
