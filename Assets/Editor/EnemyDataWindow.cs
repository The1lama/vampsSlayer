using UnityEngine;
using UnityEditor;
using System.IO;

public class EnemyDataWindow : EditorWindow
{
    private string _dataSavePath = "Assets/Prefab/Enemies";
    
    #region Window Settings
        private Vector2 _scrollPos;
        private bool _statUnscaledShow = true;
        private bool _scalingShow = true;
        private bool _preFabShow = true;
        private bool _curveShow;
        private bool _showVariance;
    #endregion

    #region Enemy Config
        private string _enemyName = "New Enemy";
        private Sprite _enemyImage;
        private float _enemyScale = 1;
        private Color _enemyHit;
    #endregion

    #region Stat Unscaled
        private int _enemyHealth = 100;
        private int _enemySpeed = 5;
        private int _enemyStrength = 10;
        private int _enemyExp = 10;
        private int _enemyScore = 10;
        private float _newSpawnTime = 1;
    #endregion

    #region Scaling Settings
        private readonly ScalingMethod[] _scalingMethod = new ScalingMethod[] {ScalingMethod.Linear, ScalingMethod.Exponential, ScalingMethod.Curve};
        private readonly string[] _scalingMethodNames = new string[] {"Linear", "Exponential", "Curve"};
        private int _selectedScalingIndex = 0;
        
        private float _perLevelMultiplier = 0.15f;
        private int _minLevel = 1;
        private float _minMultiplier = 1;
        private int _maxLevel = 20;
        private int _maxMultiplier = 3;
    #endregion
    
    #region Curve Settings
    private float _curvePreviewValue = 2;
    
    private readonly string[] _curveOptions = new string[] { "Linear", "Ease In", "Ease Out", "Custom" };
    private int _selectedHealthCurveIndex = 0;
    private int _selectedStrenghtCurveIndex = 0;
    private int _selectedSpeedCurveIndex = 0;

    private AnimationCurve _healthCurve = AnimationCurve.Linear(0, 0, 1, 1);
    private AnimationCurve _strenghtCurve = AnimationCurve.Linear(0, 0, 1, 1);
    private AnimationCurve _speedCurve = AnimationCurve.Linear(0, 0, 1, 1);
    #endregion

    #region Random Variance
        private float _randVariance = 0.05f;
    #endregion

    #region Prefab and ScriptableObject

    private GameObject _deafultPrefab;
    private GameObject _xpPrefab;
    private EnemyScriptableObject _newEnemyScriptableObject;
    
    #endregion
    
    
    [MenuItem("FG25/EnemyCreator")]
    public static void ShowWindow()
    {
        GetWindow<EnemyDataWindow>("Enemy Creator");
    }

    private void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        
        GUILayout.Label("Enemy Config", EditorStyles.boldLabel);

        _enemyName = EditorGUILayout.TextField("Name", _enemyName);
        _enemyImage = EditorGUILayout.ObjectField(_enemyImage, typeof(Sprite), false) as Sprite;
        _enemyScale = EditorGUILayout.FloatField("Scaled Enemy", _enemyScale);
        _enemyHit  = EditorGUILayout.ColorField("Hit", Color.red);

        #region PreFabs
        
        _preFabShow = EditorGUILayout.Foldout(_preFabShow, "Prefabs");
        if (_preFabShow)
        {
            _deafultPrefab =  EditorGUILayout.ObjectField(label:"Default Enemy Prefab", _deafultPrefab, typeof(GameObject), false) as GameObject;
            _xpPrefab = EditorGUILayout.ObjectField(label:"Drop Prefab", _xpPrefab, typeof(GameObject), false) as GameObject;
        }
    
        #endregion
        
        #region Stat Unscaled

                _statUnscaledShow = EditorGUILayout.Foldout(_statUnscaledShow, "Stat unscaled");
                if (_statUnscaledShow)
                {
                    _enemyHealth = EditorGUILayout.IntField("Health",_enemyHealth);
                    _enemySpeed = EditorGUILayout.IntField("Speed", _enemySpeed);
                    _enemyStrength = EditorGUILayout.IntField("Strength", _enemyStrength);
                    _enemyExp = EditorGUILayout.IntField("Experience Points", _enemyExp);
                    _enemyScore = EditorGUILayout.IntField("Score Amount", _enemyScore);
                    _newSpawnTime = EditorGUILayout.FloatField("Spawn Time", _newSpawnTime);
                    
                    GUILayout.Label(" ", EditorStyles.boldLabel);
                }

            #endregion
            
        #region Scaling Settings
        
               _scalingShow = EditorGUILayout.Foldout(_scalingShow, "Scaling Settings");
                if (_scalingShow)
                {
                    _selectedScalingIndex = EditorGUILayout.Popup("Scaling Method",  _selectedScalingIndex, _scalingMethodNames);
                    _perLevelMultiplier = EditorGUILayout.FloatField("PerLevelMultiplier", _perLevelMultiplier);
                    _minLevel = EditorGUILayout.IntField("Min Level", _minLevel);
                    _maxLevel = EditorGUILayout.IntField("Max Level", _maxLevel);
                    
                    GUILayout.Label(" ", EditorStyles.boldLabel);
                }
                
        #endregion
        
        #region Curve Settings
        
                _curveShow = EditorGUILayout.Foldout(_curveShow, "Curve Settings");
                if (_curveShow)
                {
                            _curvePreviewValue = EditorGUILayout.Slider("Preview value", _curvePreviewValue, _minLevel, _maxLevel);
                            GUILayout.Label(" ", EditorStyles.boldLabel);
                            
                            #region Health Curve
                            
                                var newHealthIndex = EditorGUILayout.Popup("Health Preset", _selectedHealthCurveIndex, _curveOptions);
                                if (newHealthIndex != _selectedHealthCurveIndex)
                                {
                                    _selectedHealthCurveIndex = newHealthIndex;
                                    _healthCurve = GetPresetCurve(_selectedHealthCurveIndex);
                                }
                    
                                // Curve field
                                _healthCurve = EditorGUILayout.CurveField("Health Curve", _healthCurve);
                    
                                // Preview the evaluated curve value at 0.5
                                var healthValue = _healthCurve.Evaluate(_curvePreviewValue);
                                EditorGUILayout.LabelField($"Health at enemy level {_curvePreviewValue}:", healthValue.ToString("F3"));
                    
                            
                            #endregion
                    
                            #region Strenght Curve
                    
                            // Dropdown
                                var newStrenghtIndex = EditorGUILayout.Popup("Strenght Preset", _selectedStrenghtCurveIndex, _curveOptions);
                                if (newStrenghtIndex != _selectedStrenghtCurveIndex)
                                {
                                    _selectedStrenghtCurveIndex = newStrenghtIndex;
                                    _strenghtCurve = GetPresetCurve(_selectedStrenghtCurveIndex);
                                }
                    
                                // Curve field
                                _strenghtCurve = EditorGUILayout.CurveField("Strength Curve", _strenghtCurve);
                    
                                // Preview the evaluated curve value at 0.5
                                var strenghtValue = _strenghtCurve.Evaluate(_curvePreviewValue);
                                EditorGUILayout.LabelField($"Strenght at enemy level {_curvePreviewValue}:", strenghtValue.ToString("F3"));
                    
                            #endregion
                            
                            #region Speed Curve
                    
                                // Dropdown
                                var newSpeedIndex = EditorGUILayout.Popup("Speed Preset", _selectedSpeedCurveIndex, _curveOptions);
                                if (newSpeedIndex != _selectedSpeedCurveIndex)
                                {
                                    _selectedSpeedCurveIndex = newSpeedIndex;
                                    _speedCurve = GetPresetCurve(_selectedSpeedCurveIndex);
                                }
                    
                                // Curve field
                                _speedCurve = EditorGUILayout.CurveField("Speed Curve", _speedCurve);
                    
                                // Preview the evaluated curve value at 0.5
                                var speedValue = _strenghtCurve.Evaluate(_curvePreviewValue);
                                EditorGUILayout.LabelField($"Speed at enemy level {_curvePreviewValue}:", speedValue.ToString("F3"));
                    
                            #endregion
                            
                            GUILayout.Label(" ", EditorStyles.boldLabel);

                }
        
        #endregion

        #region Variance slider
            _showVariance = EditorGUILayout.Foldout(_showVariance, "Variance Settings");
            if (_showVariance)
            {
                _randVariance = EditorGUILayout.Slider("Variance slider", _randVariance, -0.1f, 0.1f);
                
                GUILayout.Label(" ", EditorStyles.boldLabel);
            }

        #endregion

        
        if (CanCreateEnemy())
        {
            if (GUILayout.Button("Create Enemy"))
            {
                CreateScriptableObject();
                DuplicateAndModifyPrefab();
                ResetSomeWindowValues();
            }
            
            GUILayout.Label($"Save Path: {_dataSavePath}", EditorStyles.boldLabel);
            
        }
        else
        {
            EditorGUILayout.HelpBox("You need to name the enemy and/or add a default BaseEnemy prefab and image.", MessageType.Warning);
        }
        
        EditorGUILayout.EndScrollView();
    }
    
    private void CreateScriptableObject()
    {
        var savePath = $"{_dataSavePath}/{_enemyName}";
        _dataSavePath = savePath;
        var newEnemy = CreateInstance<EnemyScriptableObject>();
        
        #region stats unscaled
        newEnemy.enemyName = _enemyName;
        newEnemy.enemySprite = _enemyImage;
        newEnemy.enemyScale = _enemyScale;
        newEnemy.enemyHit = _enemyHit;
        newEnemy.baseHealth = _enemyHealth;
        newEnemy.baseSpeed = _enemySpeed;
        newEnemy.baseStrenght = _enemyStrength;
        newEnemy.scoreAmount = _enemyScore;
        newEnemy.experienceAmount = _enemyExp;
        newEnemy.spawnTime = _newSpawnTime;
        #endregion

        #region Scaling Settings
        newEnemy.scalingMethod = _scalingMethod[_selectedScalingIndex];
        newEnemy.perLevelMultiplier = _perLevelMultiplier;
        newEnemy.minLevel = _minLevel;
        newEnemy.maxLevel = _maxLevel;
        #endregion
            
        #region Curve Settings
            
        newEnemy.healthCurve = _healthCurve;
        newEnemy.strenghtCurve = _strenghtCurve;
        newEnemy.speedCurve = _speedCurve;
            
        #endregion

        #region Random Veriance

        newEnemy.randomVariance = _randVariance;

        #endregion
        
        if (!Directory.Exists(savePath))
        {
            Directory.CreateDirectory(savePath);
        }
                
        var savePathSo = $"{savePath}/{_enemyName}.asset";
        Debug.Log($"<Color=green>{_enemyName}</Color> Scriptable object saved to: {savePathSo}");

        AssetDatabase.CreateAsset(newEnemy, savePathSo);
        AssetDatabase.SaveAssets();

        _newEnemyScriptableObject = AssetDatabase.LoadAssetAtPath<EnemyScriptableObject>(savePathSo);
    }
    
    private void DuplicateAndModifyPrefab()
    {
        if (string.IsNullOrEmpty(_enemyName))
        {
            Debug.LogError("Please enter a new prefab name.");
            return;
        }

        string basePath = AssetDatabase.GetAssetPath(_deafultPrefab);

        // Create unique path for new prefab
        string newPrefabPath = AssetDatabase.GenerateUniqueAssetPath($"{_dataSavePath}/{_enemyName}.prefab");

        // Duplicate the prefab asset
        if (!AssetDatabase.CopyAsset(basePath, newPrefabPath))
        {
            Debug.LogError("Failed to duplicate prefab asset.");
            return;
        }

        AssetDatabase.ImportAsset(newPrefabPath);

        // Load prefab contents for editing
        GameObject prefabRoot = PrefabUtility.LoadPrefabContents(newPrefabPath);

        // Add or get PrefabHolder component
        EnemyBehaviour holder = prefabRoot.GetComponent<EnemyBehaviour>();
        if (holder == null)
            Debug.Log("No enemyBehaviour in this momnet");
        SpriteRenderer spriteRenderer = holder.GetComponent<SpriteRenderer>();
        if(spriteRenderer == null)
            Debug.Log("No sprite renderer in this momnet");

        prefabRoot.transform.localScale *= _enemyScale;
        
        // Assign the ScriptableObject reference
        holder.statSo = _newEnemyScriptableObject;
        holder.xpDrop = _xpPrefab;
        spriteRenderer.sprite = _enemyImage;


        // Save changes to the new prefab asset
        PrefabUtility.SaveAsPrefabAsset(prefabRoot, newPrefabPath);

        // Unload prefab contents from memory
        PrefabUtility.UnloadPrefabContents(prefabRoot);

        Debug.Log($"<Color=green>{_enemyName}</Color> Prefab created at: {newPrefabPath}");
    }

    private void ResetSomeWindowValues()
    {
        _dataSavePath = "Assets/Prefab/Enemies";
        _enemyName = "New Enemy";
        _enemyImage = null;
        _enemyScale = 1;
        
        #region Stat Unscaled
            _enemyHealth = 100;
            _enemySpeed = 5;
            _enemyStrength = 10;
            _enemyExp = 10;
            _enemyScore = 10;
            _newSpawnTime = 1;
        #endregion
    }

    private bool CanCreateEnemy()
    {
        return _deafultPrefab != null &&
               _enemyImage != null &&
               !string.IsNullOrWhiteSpace(_enemyName) &&
               _enemyName != "New Enemy";
    }
    
    private AnimationCurve GetPresetCurve(int curveIndex)
    {
        switch (curveIndex)
        {
            case 0: return AnimationCurve.Linear(_minLevel, _minMultiplier, _maxLevel, _maxMultiplier);
            case 1: return AnimationCurve.EaseInOut(_minLevel, _minMultiplier, _maxLevel, _maxMultiplier);
            case 2:
            {
                return new AnimationCurve(
                    new Keyframe(_minLevel, _minMultiplier, _maxLevel, _maxMultiplier),
                        new Keyframe(_minLevel, _minMultiplier, _maxLevel, _maxMultiplier)
                    );
            }
            case 3: default: return new AnimationCurve();
        }
    }
    
}
