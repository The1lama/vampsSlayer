using UnityEngine;
using UnityEditor;

public class UpgradeDataWindow : EditorWindow
{
    private string _upgradeName = "New Upgrade";
    private Sprite _upgradeSprite;
    private Color _upgradeColor;
    private string _upgradeDescription;
    private int _upgradeStat;
    private string _upgradeList;


    [MenuItem("FG25/UpgradeCreator")]
    public static void ShowWindow()
    {
        GetWindow<UpgradeDataWindow>("Upgrade Creator");
    }
    
    private void OnGUI()
    {
        GUILayout.Label("Create New Enemy", EditorStyles.boldLabel);

        _upgradeName = EditorGUILayout.TextField("Name", _upgradeName);
        _upgradeSprite = EditorGUILayout.ObjectField(_upgradeSprite, typeof(Sprite), false) as Sprite;
        _upgradeColor  = EditorGUILayout.ColorField("Hit", _upgradeColor);
        
        GUILayout.Label(" ", EditorStyles.boldLabel);

        GUILayout.Label("Stat", EditorStyles.boldLabel);
        _upgradeDescription = EditorGUILayout.TextField("Description", _upgradeDescription);
        _upgradeStat = EditorGUILayout.IntField("+ Stat", _upgradeStat);
        _upgradeList = EditorGUILayout.TextField("List", _upgradeList);
        
        
        // ReSharper disable once InvertIf
        if (GUILayout.Button("Create Upgrade"))
        {
            var newUpgrade = ScriptableObject.CreateInstance<UpgradeScriptableObject>();
            newUpgrade.upgradeName = _upgradeName;
            newUpgrade.upgradeSprite = _upgradeSprite;
            newUpgrade.upgradeColor = _upgradeColor;
            newUpgrade.upgradeDescription = _upgradeDescription;
            newUpgrade.upgradeStat = _upgradeStat;
            newUpgrade.catagory = _upgradeList;

            
            
            AssetDatabase.CreateAsset(newUpgrade, $"Assets/Data/Upgrades/{_upgradeName}.asset");
            AssetDatabase.SaveAssets();
        }
    }
}
