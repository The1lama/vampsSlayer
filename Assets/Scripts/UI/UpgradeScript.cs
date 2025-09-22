using System.Collections; 
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
// DEFINE LIST WITH UPGRADES

    [SerializeField] private UpgradeScriptableObject[] AllPowerups;


    [SerializeField] private Button Upgrade_button1;
    [SerializeField] private Button Upgrade_button2;
    [SerializeField] private Button Upgrade_button3;


    private Upgrade[] _upgrades;

    public class Upgrade
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Increase { get; set; }
        
    }
    
    private void Start()
    {
        
        
        // for (int i = 0; i < AllPowerups.Length; i++)
        // {
        //     _upgrades = new Upgrade
        //     {
        //         Name = AllPowerups[i].upgradeName,
        //         Description = AllPowerups[i].upgradeDescription,
        //         Increase = AllPowerups[i].upgradeStat,
        //     };
        // }
        //
        // Debug.Log(_upgrades);
        
        
        ButtonsSet();
    }

    public void ButtonsSet()
    {
        Debug.Log("Hello,World");
        Debug.Log(AllPowerups);


        // CHOOSING UPGRADE FROM UPGRADE ARRAY
        List<int> availableUpgrades = new List<int>();
        for (int i = 0; i < AllPowerups.Length; i++)
        {
            availableUpgrades.Add(i);
        }

        ShuffleList(availableUpgrades);
        UpgradeScriptableObject Upgrade_1 = AllPowerups[availableUpgrades[0]];
        UpgradeScriptableObject Upgrade_2 = AllPowerups[availableUpgrades[1]];
        UpgradeScriptableObject Upgrade_3 = AllPowerups[availableUpgrades[2]];

        // Setting text
        Upgrade_button1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Upgrade_1.upgradeName +
            "\n==========\n" + Upgrade_1.upgradeDescription + "\n" + Upgrade_1.upgradeStat;
        Upgrade_button2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Upgrade_2.upgradeName +
            "\n==========\n" + Upgrade_2.upgradeDescription + "\n" + Upgrade_2.upgradeStat;
        Upgrade_button3.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = Upgrade_3.upgradeName +
            "\n==========\n" + Upgrade_3.upgradeDescription + "\n" + Upgrade_3.upgradeStat;
        


        // Replacing the X with increase value
        // Upgrade_DescriptionText1.text = Upgrade_1.Description.Replace("X", Upgrade_1.Increase.ToString());
        // Upgrade_DescriptionText2.text = Upgrade_2.Description.Replace("X", Upgrade_2.Increase.ToString());
        // Upgrade_DescriptionText3.text = Upgrade_3.Description.Replace("X", Upgrade_3.Increase.ToString());
        //     Upgrade_DescriptionText4.text = Upgrade_4.Description.Replace("X", Upgrade_4.Increase.ToString());
        //
        //     // Setting color of the buttons
        //     Dictionary<string, Color> rarityColors = new Dictionary<string, Color>();
        //     rarityColors.Add("Common", new Color(1, 1, 1, 1));
        //     rarityColors.Add("Rare", new Color(0.5f, 1f, 0.5f, 1));
        //     rarityColors.Add("Epic", new Color(0.75f, 0.25f, 0.75f, 1));
        //
        //
        //     Upgrade_button1.GetComponent<Image>().color = rarityColors[Upgrade_1.Rarity];
        //     Upgrade_button2.GetComponent<Image>().color = rarityColors[Upgrade_2.Rarity];
        //     Upgrade_button3.GetComponent<Image>().color = rarityColors[Upgrade_3.Rarity];
        //     Upgrade_button4.GetComponent<Image>().color = rarityColors[Upgrade_4.Rarity];
    }
        //
        // // UPGRADES
    public void UpgradeChosen(string Upgrade_chosen)
    {
        switch (Upgrade_chosen)
        {
            case "Attack speed (projectiles)":
                // Attack_speed += increase;
                Debug.Log("Attack speed (projectiles)");
                break;
            case "Projectile damage":
                Debug.Log("Projectile damage");
                break;
            case "Projectile size":
                Debug.Log("Projectile size");
                break;
            case "Hello":
                Debug.Log("Hello");
                break;
            case "Precision":
                Debug.Log("Precision");
                break;
            case "Train health":
                Debug.Log("Train health");
                break;
            case "Train repair":
                Debug.Log("Train repair");
                break;
            case "Level up faster":
                Debug.Log("Level up faster");
                break;
            case "Greater view":
                Debug.Log("Greater view");
                break;
            case "Area damage":
                Debug.Log("Area damage");
                break;
            case "Crit chance":
                Debug.Log("Crit chance");
                break;
            case "Crit multiplier":
                Debug.Log("Crit multiplier");
                break;
        }
    }
    
    // SHUFFLE LIST
    public void ShuffleList(List<int> list)
    {
        for (int i = 0; i < list.Count; i++) 
        {
            int randomIndex = Random.Range(i, list.Count);
            int temp = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }
}

