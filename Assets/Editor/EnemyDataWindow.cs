using UnityEngine;
using UnityEditor;

public class EnemyDataWindow : EditorWindow
{
    private string _enemyName = "New Enemy";
    
    [MenuItem("FG25/EnemyCreator")]
    public static void ShowWindow()
    {
        GetWindow<EnemyDataWindow>("Enemy Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Enemy", EditorStyles.boldLabel);

        _enemyName = EditorGUILayout.TextField("Name", _enemyName);
        
        GUILayout.Label("Stat", EditorStyles.boldLabel);
        var enemyHealth = EditorGUILayout.IntField("Health",100);
        var enemySpeed = EditorGUILayout.IntField("Speed", 5);
        var enemyStrength = EditorGUILayout.IntField("Strength", 10);
        
        GUILayout.Label("Game Setting", EditorStyles.boldLabel);
        var enemyExp = EditorGUILayout.IntField("Experience Points", 10);
        var enemyScore = EditorGUILayout.IntField("Score Amount", 20);
        

        
        
        // ReSharper disable once InvertIf
        if (GUILayout.Button("Create Enemy"))
        {
            var newEnemy = ScriptableObject.CreateInstance<EnemyScriptableObject>();
            newEnemy.enemyName = _enemyName;
            newEnemy.health = enemyHealth;
            newEnemy.speed = enemySpeed;
            newEnemy.strenght = enemyStrength;
            newEnemy.experienceAmount = enemyExp;
            newEnemy.scoreAmount = enemyScore;
            
            
            AssetDatabase.CreateAsset(newEnemy, $"Assets/Data/Enemies/{_enemyName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
    
}
