using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    private bool _isPlayerDead;
    
    
    [Tooltip("How far away from player enemies spawn")]
    [SerializeField] private float spawnRadius;
    private float _angleInDegrees;


    private void Start()
    {
        GameManager.Instance.onDeath.AddListener( () => _isPlayerDead = true );
    }
    
    public IEnumerator SpawnRoutine(GameObject enemyPrefab)
    {
        // GameObject enemyPrefab = enemies[enemyListPosition];
        
        //while player is alive
        while (!_isPlayerDead)
        {
            // Check enemyLevel from gameManager everytime it should be spawning new enemy
            int level = GameManager.Instance.EnemyLevels;
            _angleInDegrees = Random.Range(0, 360);
            
            // Calculate the spawnposition for the enemy and puts it on a vector3
            Vector2 positionSpawn = GetPositionOnCircle(spawnRadius, _angleInDegrees, transform.position);
            Vector3 spawnHere = new Vector3(positionSpawn.x, positionSpawn.y, 0);
            
            // spawns enemy and returns a gameobject to Initialize enemy stats, and get spawnDelay.
            var enemy = Instantiate(enemyPrefab, spawnHere, Quaternion.identity);
            enemy.GetComponent<EnemyBehaviour>().Initialize(level);
            var spawnDelay = enemy.GetComponent<EnemyBehaviour>().spawnDelay;
            
            // wait for new spawn
            yield return new WaitForSeconds(spawnDelay);
        }
    }

    

/// <summary>
/// Creates a radius around an object 
/// </summary>
/// <param name="radius">Radius distance around the object</param>
/// <param name="positionAngleDegrees">The position angle round the object; 180 Behind, 0/360 Front</param>
/// <param name="centerPoint">The objects position Vector2</param>
/// <returns>Vector2 position around the object with the given radius</returns>
    
    private static Vector2 GetPositionOnCircle(float radius, float positionAngleDegrees, Vector2 centerPoint)
    {
        float angleInRadians = positionAngleDegrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRadians) * radius + centerPoint.x;
        float y = Mathf.Sin(angleInRadians) * radius + centerPoint.y;
        
        return new Vector2(x, y);
    }
/// <summary>
/// Creates a radius around an object 
/// </summary>
/// <param name="radius">Radius distance around the object</param>
/// <param name="positionAngleDegrees">The position angle round the object; 180 Behind, 0/360 Front</param>
/// <returns>Vector2 position around the object with the given radius</returns>
    private static Vector2 GetPositionOnCircle(float radius, float positionAngleDegrees)
    {
        float angleInRadians = positionAngleDegrees * Mathf.Deg2Rad;
        float x = Mathf.Cos(angleInRadians) * radius;
        float y = Mathf.Sin(angleInRadians) * radius;
        
        return new Vector2(x, y);
    }
    
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(GetPositionOnCircle(spawnRadius, _angleInDegrees, transform.position), Vector3.one);
        Gizmos.DrawWireSphere(transform.position, spawnRadius);

    }
}
