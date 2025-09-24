using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class UppgradeDisplay : MonoBehaviour
{

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;
    public TextMeshProUGUI upgradeNumText;



    public void SetUpUpgradeCard(UpgradeScriptableObject card)
    {
        Debug.Log("Initializing upgrade card");
        nameText.text = card.upgradeName;
        descriptionText.text = card.upgradeDescription;
        upgradeNumText.text = card.upgradeStat.ToString("00.0");
    }



}
