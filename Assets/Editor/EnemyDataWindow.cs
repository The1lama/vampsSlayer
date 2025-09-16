using UnityEngine;
using UnityEditor;

public class EnemyDataWindow : EditorWindow
{
    private string enemyName = "New Enemy";
    
    [MenuItem("Examples/This")]
    public static void ShowWindow()
    {
        GetWindow<EnemyDataWindow>("Enemy Creator");
    }

    void OnGUI()
    {
        GUILayout.Label("Create New Enemy", EditorStyles.boldLabel);

        enemyName = EditorGUILayout.TextField("Name", enemyName);
        var enemyHealth = EditorGUILayout.IntField("Health", 100);
        var enemySpeed = EditorGUILayout.IntField("Speed", 5);
        var enemyStrength = EditorGUILayout.IntField("Strength", 10);

        

        if (GUILayout.Button("Create Enemy"))
        {
            Enemy newEnemy = ScriptableObject.CreateInstance<Enemy>();
            newEnemy.EnemyName = enemyName;
            newEnemy.Health = enemyHealth;
            newEnemy.Speed = enemySpeed;
            newEnemy.Strenght = enemyStrength;
            
            AssetDatabase.CreateAsset(newEnemy, $"Assets/Data/Enemies/{enemyName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
    
}
