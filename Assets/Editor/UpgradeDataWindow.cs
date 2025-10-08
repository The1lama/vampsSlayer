using UnityEngine;
using UnityEditor;

public class UpgradeDataWindow : EditorWindow
{
    
    private string _dataSavePath = "Assets/Upgrades";
    private Vector2 _scrollPos;

    private string _upgradeName = "New Upgrade";
    private Sprite _upgradeSprite;
    private Color _upgradeColor;
    private string _upgradeDescription;
    private int _upgradeStat;


    [MenuItem("FG25/UpgradeCreator")]
    public static void ShowWindow()
    {
        GetWindow<UpgradeDataWindow>("Upgrade Creator");
    }
    
    private void OnGUI()
    {
        _scrollPos = EditorGUILayout.BeginScrollView(_scrollPos);
        
        GUILayout.Label("Create New Enemy", EditorStyles.boldLabel);

        _upgradeName = EditorGUILayout.TextField("Name", _upgradeName);
        _upgradeSprite = EditorGUILayout.ObjectField("Upgrade Sprite", _upgradeSprite, typeof(Sprite), false) as Sprite;
        _upgradeColor  = EditorGUILayout.ColorField("Background Color", _upgradeColor);
        
        GUILayout.Label(" ", EditorStyles.boldLabel);

        GUILayout.Label("Upgrade Info", EditorStyles.boldLabel);
        _upgradeDescription = EditorGUILayout.TextField("Description", _upgradeDescription);
        _upgradeStat = EditorGUILayout.IntField("Amount to Increase", _upgradeStat);
        
        
        // ReSharper disable once InvertIf
        if (CanCreateUpgrade())
        {
            if (GUILayout.Button("Create upgrade"))
            {
                CreateUpgrade();
                ResetSomeWindowValues();
            }
            
            GUILayout.Label($"Save Path: {_dataSavePath}", EditorStyles.boldLabel);
            EditorGUILayout.HelpBox($"Don't forget to add the upgrade to upgrade list in \nLevelUpMenu > Upgrades", MessageType.Warning);


        }
        else
        {
            EditorGUILayout.HelpBox("You need to name the upgrade and/or add a Description", MessageType.Warning);
        }
        
        EditorGUILayout.EndScrollView();
    }

    private void ResetSomeWindowValues()
    {
        _upgradeName = "New Upgrade";
        _upgradeSprite = null;
        _upgradeDescription = "";
        _upgradeStat = 0;
    }

    private void CreateUpgrade()
    {
        var newUpgrade = ScriptableObject.CreateInstance<UpgradeScriptableObject>();
        newUpgrade.upgradeName = _upgradeName;
        newUpgrade.upgradeSprite = _upgradeSprite;
        newUpgrade.upgradeColor = _upgradeColor;
        newUpgrade.upgradeDescription = _upgradeDescription;
        newUpgrade.upgradeStat = _upgradeStat;
            
        AssetDatabase.CreateAsset(newUpgrade, $"Assets/Data/Upgrades/{_upgradeName}.asset");
        AssetDatabase.SaveAssets();
    }

    private bool CanCreateUpgrade()
    {
        return _upgradeName != null && _upgradeDescription != null;
    }
}
