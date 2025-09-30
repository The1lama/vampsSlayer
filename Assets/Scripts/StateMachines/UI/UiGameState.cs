using UnityEngine;

public class UiGameState : UiBaseState
{
    public GameObject UiCanvas;
    
    
    public override void EnterState(UiStateManager ui)
    {
        Debug.Log("Entered UI state");
        UiCanvas.SetActive(true);
    }
    
    
    public override void UpdateState(UiStateManager ui)
    {
    }
    
    public override void ExitState(UiStateManager ui)
    {
        UiCanvas.SetActive(false);
    }
}
