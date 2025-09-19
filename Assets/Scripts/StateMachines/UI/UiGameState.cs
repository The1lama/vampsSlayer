using Unity.VisualScripting;
using UnityEngine;

public class UiGameState : UiBaseState
{
    // GameObject uiCanvas = GameObject.Find("HUD");
    
    public override void EnterState(UiStateManager ui )
    {
        // uiCanvas.SetActive(true);
    }
    
    
    public override void UpdateState(UiStateManager ui)
    {
    }
    
    public override void ExitState(UiStateManager ui)
    {
        // uiCanvas.SetActive(false);

    }
}
