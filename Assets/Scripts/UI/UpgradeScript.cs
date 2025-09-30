using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class UpgradeScript : MonoBehaviour
{
// DEFINE LIST WITH UPGRADES

    // [SerializeField] private UpgradeScriptableObject[] allPowerups;
    [SerializeField] private List<UpgradeScriptableObject> powerUps;

    [SerializeField] private Button upgradeButton1;
    [SerializeField] private Button upgradeButton2;
    [SerializeField] private GameObject levelUpCanvas;
    [SerializeField] private GameObject player;

    private UiStateManager _globalStateManager;
    
    
    private void Start()
    {
        // Checks if there is powerups in list 
        if (powerUps == null || powerUps.Count == 0)
        {
            Debug.LogError($"No Powerups Set; {name}"); return;
        }
        
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
        
        
        
        // CHOOSING UPGRADE FROM UPGRADE ARRAY
        List<int> _availableUpgrades = new List<int>();

        for (int i = 0; i < powerUps.Count; i++)
        {
            _availableUpgrades.Add(i);
        }

        ShuffleList(_availableUpgrades);
        UpgradeScriptableObject upgrade1 = powerUps[_availableUpgrades[0]];
        UpgradeScriptableObject upgrade2 = powerUps[_availableUpgrades[1]];

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
        // string[] thing = upgradeChosen.Split("\n");
        
        switch (upgradeChosen)
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
                if (player.GetComponent<PlayerAttack>().GetMeleeSpeed() <= 0.2f)
                {
                    foreach (var objSo in powerUps)
                    {
                        if (objSo.name != upgradeChosen) continue;
                        powerUps.Remove(objSo);
                        break;
                    }
                }
                break;
            
            case "Health":
                player.GetComponent<PlayerBehaviour>().SetNewMaxHealth((int)addStat);
                Debug.Log(upgradeChosen);
                Debug.Log(addStat);
                break;
           default:
               Debug.LogWarning("Could not find chosen upgrade name: " + upgradeChosen);
               break;
        }
        
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

