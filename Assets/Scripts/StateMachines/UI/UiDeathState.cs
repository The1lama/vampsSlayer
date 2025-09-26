using UnityEngine;

public class UiDeathState : UiBaseState
{
    // public GameObject DeathCanvas;
    
    
    public override void EnterState(UiStateManager ui)
    {
        Debug.Log("Entered DeathState");
        ui.deathCanvas.SetActive(true);
        // DeathCanvas.SetActive(true);
    }
    
    
    public override void UpdateState(UiStateManager ui)
    {
        
    }
    
    public override void ExitState(UiStateManager ui)
    {
        ui.deathCanvas.SetActive(false);
        // DeathCanvas.SetActive(false);
    }
}
