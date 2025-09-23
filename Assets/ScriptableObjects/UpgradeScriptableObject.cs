using UnityEngine;

[CreateAssetMenu(fileName = "Upgrade", menuName = "FG25/Upgrade")]
public class UpgradeScriptableObject : ScriptableObject
{
        [Header("Basics")]
        public string upgradeName;
        public Sprite upgradeSprite;
        public Color upgradeColor;
        public string upgradeDescription;

        [Header("Upgrade stat")] 
        public float upgradeStat;
        

}
