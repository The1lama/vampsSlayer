using UnityEngine;


[CreateAssetMenu(fileName = "Player", menuName = "FG25/Player")]
public class PlayerScriptableObject :  ScriptableObject
{
   
    [Header("Basic Stat")]
    public int health;
    public float speed;
    public int strenght;
    public float attackSpeed;
    public float iFrameTime;
    public Color hitTint;
}
