using UnityEngine;

public class UiOptionsState : UiBaseState
{
    
    public GameObject OptionsCanvas;
    
    public override void EnterState(UiStateManager ui)
    {

        Debug.Log("Entered OptionsState");
        Time.timeScale = 0;
        OptionsCanvas.SetActive(true);
    }
    
    public override void UpdateState(UiStateManager ui)
    {
        
    }
    
    public override void ExitState(UiStateManager ui)
    {
        Time.timeScale = 1;
        OptionsCanvas.SetActive(false);
    }
}
