using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    
    [SerializeField] private GameObject[] enemies;
    [SerializeField] private float spawnDelay = 1;

    [SerializeField] private GameObject enemyContainer;
    
    [SerializeField] private float spawnRadius;
    private float _angleInDegrees;
    
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
            _angleInDegrees = Random.Range(0, 360);
            Vector2 positionSpawn = GetPositionOnCircle(spawnRadius, _angleInDegrees, transform.position);
            Vector3 spawnHere = new Vector3(positionSpawn.x, positionSpawn.y, 0);
            GameObject newEnemy = Instantiate(enemies[0], spawnHere, Quaternion.identity);
            newEnemy.transform.parent = enemyContainer.transform;
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

    }
}
