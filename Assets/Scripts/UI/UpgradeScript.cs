using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
// DEFINE LIST WITH UPGRADES

    [SerializeField] private UpgradeScriptableObject[] allPowerups;


    [SerializeField] private Button upgradeButton1;
    [SerializeField] private Button upgradeButton2;
    [SerializeField] private GameObject levelUpCanvas;

    
    
    private Upgrade[] _upgrades;

    public class Upgrade
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Increase { get; set; }
        
    }
    
    private void Start()
    {
        
        upgradeButton1.onClick.AddListener(() => ButtonOnClick(upgradeButton1));
        upgradeButton2.onClick.AddListener(() => ButtonOnClick(upgradeButton2));
        
        
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

    private void ButtonOnClick(Button clickedButton)
    {
        string upgradeChosen = clickedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        UpgradeChosen(upgradeChosen);
        GameManager.Instance.transform.GetComponent<UIScript>().GamePause();
    }


    public void ButtonsSet()
    {
        // Checks if there is powerups in list 
        if (allPowerups == null || allPowerups.Length == 0)
        {
            Debug.LogError("No Powerups Set"); return;
        }
        
        
        // CHOOSING UPGRADE FROM UPGRADE ARRAY
        List<int> availableUpgrades = new List<int>();
        for (int i = 0; i < allPowerups.Length; i++)
        {
            availableUpgrades.Add(i);
        }

        ShuffleList(availableUpgrades);
        UpgradeScriptableObject upgrade1 = allPowerups[availableUpgrades[0]];
        UpgradeScriptableObject upgrade2 = allPowerups[availableUpgrades[1]];

        // Setting text
        // upgradeButton1.transform.GetChild(0).gameObject.GetComponent<UppgradeDisplay>().SetUpUpgradeCard(upgrade1);
        upgradeButton1.GetComponent<UppgradeDisplay>().SetUpUpgradeCard(upgrade1);
        upgradeButton2.GetComponent<UppgradeDisplay>().SetUpUpgradeCard(upgrade2);

                // upgradeButton1.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgrade1.upgradeName +
        //     "\n==========\n" + upgrade1.upgradeDescription + "\n" + upgrade1.upgradeStat;
        // upgradeButton2.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = upgrade2.upgradeName +
        //     "\n==========\n" + upgrade2.upgradeDescription + "\n" + upgrade2.upgradeStat;
        
        // Setting color for buttons
        upgradeButton1.GetComponent<Image>().color = upgrade1.upgradeColor;
        upgradeButton2.GetComponent<Image>().color = upgrade2.upgradeColor;
    }

    // UPGRADES
    public void UpgradeChosen(string upgradeChosen)
    {
        string[] thing = upgradeChosen.Split("\n");
        
        switch (thing[0])
        {
            case "Strenght":
                Debug.Log("Strenght");
                Debug.Log(thing.Last());
                break;
            case "Speed":
                Debug.Log("Speed");
                Debug.Log(thing.Last());
                break;
            case "AttackSpeed":
                Debug.Log("AttackSpeed");
                Debug.Log(thing.Last());
                break;
            case "Health":
                Debug.Log("AttackSpeed");
                Debug.Log(thing.Last());
                break;
           default:
               Debug.LogWarning("Could not find chosen upgrade name: " + thing[0]);
               break;
        }

        if (levelUpCanvas.activeInHierarchy)
        {
            levelUpCanvas.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Level up canvas was already of: In UpgradeScript.cs" );
        }
        
        
    }
    
    // SHUFFLE LIST
    private void ShuffleList(List<int> list)
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

