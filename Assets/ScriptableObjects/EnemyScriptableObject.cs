using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Enemy", menuName = "FG25/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Name of Enemy")]
    public string enemyName;
    
    [Header("Basic Stat")]
    public int health;
    public int speed;
    public int strenght;

    [Header("Game Stat")]
    public int experienceAmount;
    public int scoreAmount;
 
}
