using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(fileName = "Enemy", menuName = "FG25/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    public string enemyName;
    public int health;
    public int speed;
    public int strenght;

    public int experienceAmount;
    public int scoreAmount;
    
    public Sprite image;

}
