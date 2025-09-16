using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "FG25/Enemy")]
public class Enemy : ScriptableObject
{
    public string EnemyName;
    public int Health;
    public int Speed;
    public int Strenght;

    public Sprite Image;

}
