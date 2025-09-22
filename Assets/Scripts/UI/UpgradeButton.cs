using TMPro;
using UnityEngine;

public class UpgradeButton : MonoBehaviour
{
    private UpgradeScript _upgradeScript;
    
    void Start()
    {
        _upgradeScript = transform.parent.GetComponent<UpgradeScript>();
    }
    
    public void UpgradeB()
    {
        string upgradeChosen = transform.GetChild(0).GetComponent<TextMeshProUGUI>().text;
        _upgradeScript.UpgradeChosen(upgradeChosen);
        GameManager.Instance.transform.GetComponent<UIScript>().PauseFunc();
    }
    
    
}
