using UnityEngine;


[CreateAssetMenu(fileName = "Enemy", menuName = "FG25/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Basics of Enemy")]
    public string enemyName;
    public Sprite enemySprite;
    public Color enemyHit;
    
    [Header("Basic Stat")]
    public int health;
    public int speed;
    public int strenght;

    [Header("Game Stat")] 
    public int level;
    public int experienceAmount;
    public int scoreAmount;

    public void LevelUp()
    {
        level++;
        health += 10;
        strenght += 5;
        speed += 10;
    }
    
    
}
