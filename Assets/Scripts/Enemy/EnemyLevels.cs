using UnityEngine;
using UnityEngine.Events;

public class EnemyLevels : MonoBehaviour
{
    private int _enemyLevel = 1;

    private UnityEvent _enemyLevelUp;
    public UnityEvent _enemyCalls; 

    private void Awake()
    {
        _enemyLevelUp.AddListener( EnemyLevelUp );
    }

    private void Start()
    {
        
    }
    
    
    public void EnemyLevelUp()
    {
        _enemyLevel++;
    }
    
}
