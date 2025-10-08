using UnityEngine;


public enum ScalingMethod {Linear, Exponential, Curve}


[CreateAssetMenu(fileName = "Enemy", menuName = "FG25/Enemy")]
public class EnemyScriptableObject : ScriptableObject
{
    [Header("Enemy Config")]
    public string enemyName;
    public Sprite enemySprite;
    public float enemyScale;
    public Color enemyHit;
    
    [Header("Basic Stat (unscaled")]
    public int baseHealth;
    public int baseSpeed;
    public int baseStrenght;
    public int experienceAmount;
    public int scoreAmount;
    public float spawnTime;

    [Header("Scaling")]
    public ScalingMethod scalingMethod = ScalingMethod.Linear;
    [Tooltip("Used by Linear and Exponential")]
    public float perLevelMultiplier = 0.15f; // +15% per level for linear, base for exponent for exponential
    [Tooltip("Minimum level to start scaling")]
    public int minLevel = 1;
    [Tooltip("Cap level to prevent runaway scaling (0 = no cap)")]
    public int maxLevel = 10;
    
    [Header("Curve (if using Curve)")]
    public AnimationCurve curve = AnimationCurve.Linear(0, 1, 10, 2); // multiplier over level;
    
    [Header("Random variance")]
    [Range(0f, 0.5f)] public float randomVariance = 0.05f; // ±5%
    
}
