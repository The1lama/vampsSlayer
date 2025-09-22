using UnityEngine;

public class UiPauseState : UiBaseState
{
    
    public GameObject PauseCanvas;
    
    public override void EnterState(UiStateManager ui)
    {
        Debug.Log("Entered PauseState");
        PauseCanvas.SetActive(true);
    }
    
    public override void UpdateState(UiStateManager ui)
    {
        
    }
    
    public override void ExitState(UiStateManager ui)
    {
        PauseCanvas.SetActive(false);
    }
}
