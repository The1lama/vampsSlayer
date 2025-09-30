using System;
using System.Collections;
using System.Linq;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnDelay = 1;

    [SerializeField] private GameObject enemyContainer;
    
    void Start()
    {
        if (enemies == null || enemies.Length == 0)
        {
            Debug.LogWarning($"No enemies assigned, in <color=yellow>{name}</color>");
        }
        
        
        StartCoroutine(SpawnRoutine());
    }
    
    
    IEnumerator SpawnRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(spawnDelay);
            Vector3 currentPosition = transform.position;
            Vector3 spawnHere = new Vector3(currentPosition.x += Random.Range(-8f, 8f), currentPosition.y, 0);
            GameObject newEnemy = Instantiate(enemies[0], spawnHere, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.coral;
        Gizmos.DrawWireSphere(transform.position, 8f);
    }
}
