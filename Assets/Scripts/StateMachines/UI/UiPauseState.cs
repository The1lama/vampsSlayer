using UnityEngine;

public class UiPauseState : UiBaseState
{
    
    public GameObject PauseCanvas;
    private bool _isPaused;
    
    public override void EnterState(UiStateManager ui)
    {

        Debug.Log("Entered PauseState");
        Time.timeScale = 0;
        PauseCanvas.SetActive(true);
    }
    
    public override void UpdateState(UiStateManager ui)
    {
        
    }
    
    public override void ExitState(UiStateManager ui)
    {
        Time.timeScale = 1;
        PauseCanvas.SetActive(false);
    }
}
