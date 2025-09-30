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
    [SerializeField] private GameObject player;

    private UiStateManager _globalStateManager;
    
    
    private void Start()
    {
        upgradeButton1.onClick.AddListener(() => ButtonOnClick(upgradeButton1));
        upgradeButton2.onClick.AddListener(() => ButtonOnClick(upgradeButton2));
    }

    private void ButtonOnClick(Button clickedButton)
    {
        string upgradeChosen = clickedButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        float addStat = float.Parse(clickedButton.transform.GetChild(3).GetComponent<TextMeshProUGUI>().text);
        UpgradeChosen(upgradeChosen, addStat);
    }


    public void ButtonsSet(UiStateManager stateManager)
    {
        _globalStateManager = stateManager;
        
        // Checks if there is powerups in list 
        if (allPowerups == null || allPowerups.Length == 0)
        {
            Debug.LogError("No Powerups Set"); return;
        }
        
        
        // CHOOSING UPGRADE FROM UPGRADE ARRAY
        List<int> _availableUpgrades = new List<int>();

        for (int i = 0; i < allPowerups.Length; i++)
        {
            _availableUpgrades.Add(i);
        }

        ShuffleList(_availableUpgrades);
        UpgradeScriptableObject upgrade1 = allPowerups[_availableUpgrades[0]];
        UpgradeScriptableObject upgrade2 = allPowerups[_availableUpgrades[1]];

        // Setting text
        upgradeButton1.GetComponent<UppgradeDisplay>().SetUpUpgradeCard(upgrade1);
        upgradeButton2.GetComponent<UppgradeDisplay>().SetUpUpgradeCard(upgrade2);

        // Setting color for buttons
        upgradeButton1.GetComponent<Image>().color = upgrade1.upgradeColor;
        upgradeButton2.GetComponent<Image>().color = upgrade2.upgradeColor;
    }

    // UPGRADES
    public void UpgradeChosen(string upgradeChosen, float  addStat)
    {
        string[] thing = upgradeChosen.Split("\n");
        
        switch (thing[0])
        {
            case "Strenght":
                player.GetComponent<PlayerAttack>().SetStrenght((int)addStat);
                Debug.Log(upgradeChosen);
                Debug.Log(addStat);
                break;
            case "Speed":
                player.GetComponent<PlayerMovement>().SetSpeed((int)addStat);
                Debug.Log(upgradeChosen);
                Debug.Log(addStat);
                break;
            case "AttackSpeed":
                player.GetComponent<PlayerAttack>().SetNewMeleeSpeed(addStat);
                Debug.Log(upgradeChosen);
                Debug.Log(addStat);
                break;
            case "Health":
                player.GetComponent<PlayerBehaviour>().SetNewMaxHealth((int)addStat);
                Debug.Log(upgradeChosen);
                Debug.Log(addStat);
                break;
           default:
               Debug.LogWarning("Could not find chosen upgrade name: " + thing[0]);
               break;
        }

        // ShuffleList(_availableUpgrades);

        
        if (levelUpCanvas.activeInHierarchy)
        {
            _globalStateManager.SwitchState(_globalStateManager.GameState);
        }
        else
        {
            Debug.LogWarning("Level up canvas was already turned off: <Color=yellow>In UpgradeScript.cs</Color>" );
        }
    }
    
    // SHUFFLE LIST
    private void ShuffleList(List<int> list)
    {
        Debug.Log("Shuffling list");
        for (int i = 0; i < list.Count; i++) 
        {
            int randomIndex = Random.Range(i, list.Count);
            (list[i], list[randomIndex]) = (list[randomIndex], list[i]);
        }
    }


}

