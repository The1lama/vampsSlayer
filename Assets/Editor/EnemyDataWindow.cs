using UnityEngine;
using UnityEditor;

public class EnemyDataWindow : EditorWindow
{
    private string _enemyName = "New Enemy";
    private int _enemyHealth = 100;
    private int _enemySpeed = 5;
    private int _enemyStrength = 10;
    private int _enemyExp = 10;
    private int _enemyScore = 10;

    private Sprite _enemyImage;
    
    
    [MenuItem("FG25/EnemyCreator")]
    public static void ShowWindow()
    {
        GetWindow<EnemyDataWindow>("Enemy Creator");
    }

    private void OnGUI()
    {
        GUILayout.Label("Create New Enemy", EditorStyles.boldLabel);

        _enemyName = EditorGUILayout.TextField("Name", _enemyName);
        _enemyImage = EditorGUILayout.ObjectField(_enemyImage, typeof(Sprite), false) as Sprite;
        
        GUILayout.Label(" ", EditorStyles.boldLabel);

        GUILayout.Label("Stat", EditorStyles.boldLabel);
        _enemyHealth = EditorGUILayout.IntField("Health",_enemyHealth);
        _enemySpeed = EditorGUILayout.IntField("Speed", _enemySpeed);
        _enemyStrength = EditorGUILayout.IntField("Strength", _enemyStrength);
        
        GUILayout.Label(" ", EditorStyles.boldLabel);

        GUILayout.Label("Game Setting", EditorStyles.boldLabel);
        _enemyExp = EditorGUILayout.IntField("Experience Points", _enemyExp);
        _enemyScore = EditorGUILayout.IntField("Score Amount", _enemyScore);
        

        
        
        // ReSharper disable once InvertIf
        if (GUILayout.Button("Create Enemy"))
        {
            var newEnemy = ScriptableObject.CreateInstance<EnemyScriptableObject>();
            newEnemy.enemyName = _enemyName;
            newEnemy.enemySprite = _enemyImage;
            newEnemy.health = _enemyHealth;
            newEnemy.speed = _enemySpeed;
            newEnemy.strenght = _enemyStrength;
            newEnemy.experienceAmount = _enemyExp;
            newEnemy.scoreAmount = _enemyScore;
            
            
            AssetDatabase.CreateAsset(newEnemy, $"Assets/Data/Enemies/{_enemyName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
    
}
