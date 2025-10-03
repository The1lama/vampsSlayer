using UnityEngine;

public static class StatScaler
{
    // safe integer clamp
    public static int ClampMax(int val, int max) => (max > 0) ? Mathf.Min(val, max) : val;


    /// <summary>
    /// apply some math for scaling enemy level
    /// </summary>
    /// <param name="baseValue">Base stat value</param>
    /// <param name="level">Level to scale the stat</param>
    /// <param name="stats">Scriptable Object to take from</param>
    /// <param name="curve">What scaling curve to use {Linear, Exponential, Curve}</param>
    /// <param name="methodOverride">Don't know</param>
    /// <returns>Returns the scaled value of the base stat</returns>
    public static float ApplyScaling(float baseValue, int level, EnemyScriptableObject stats, AnimationCurve curve = null, ScalingMethod? methodOverride = null)
    {
        var method = methodOverride ?? stats.scalingMethod;
        int effectiveLevel = Mathf.Max(level, stats.minLevel);
        effectiveLevel = ClampMax(effectiveLevel, stats.maxLevel);

        float multiplier = 1f;

        switch (method)
        {
            case ScalingMethod.Linear:
                // multiplier = 1 + perLevelMultiplier * (level - 1)
                multiplier = 1f + stats.perLevelMultiplier * (effectiveLevel - 1);
                break;
            case ScalingMethod.Exponential:
                // multiplier = (1 + perLevelMultiplier)^(level - 1)
                multiplier = Mathf.Pow(1f + stats.perLevelMultiplier, effectiveLevel - 1);
                break;
            case ScalingMethod.Curve:
                if (curve != null) multiplier = curve.Evaluate(effectiveLevel);
                else multiplier = 1f;
                break;
        }

        // result before randomness
        float result = baseValue * multiplier;

        // apply small random variance
        if (stats.randomVariance > 0f)
        {
            float variance = Random.Range(1f - stats.randomVariance, 1f + stats.randomVariance);
            result *= variance;
        }

        return result;
    }
}
