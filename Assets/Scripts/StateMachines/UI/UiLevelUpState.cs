using UnityEngine;

public class UiLevelUpState : UiBaseState
{
    
    public GameObject LevelUpCanvas;
    
    
    public override void EnterState(UiStateManager ui)
    {
        Time.timeScale = 0;
        LevelUpCanvas.GetComponentInChildren<UpgradeScript>().ButtonsSet(ui);
        LevelUpCanvas.SetActive(true);
    }

    public override void UpdateState(UiStateManager ui)
    {
        throw new System.NotImplementedException();
    }

    public override void ExitState(UiStateManager ui)
    {
        Debug.Log("<Color=Red>Exiting state</Color>");
        
        Time.timeScale = 1;
        
        LevelUpCanvas.SetActive(false);
    }
}
