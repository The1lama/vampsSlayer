using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject smallZombie;
    [SerializeField] private float spawnDelay = 1;

    [SerializeField] private GameObject enemyContainer;
    
    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }
    
    
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            Vector3 currentPosition = transform.position;
            Vector3 spawnHere = new Vector3(currentPosition.x += Random.Range(-8f, 8f), currentPosition.y, 0);
            GameObject newEnemy = Instantiate(smallZombie, spawnHere, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
        }
    }
    
}
