using UnityEngine;

public class UiDeathState : UiBaseState
{
    public GameObject DeathCanvas;
    public DeathScore DeathScore;
    
    
    public override void EnterState(UiStateManager ui)
    {
        Debug.Log("Entered DeathState");
        GameManager.Instance.Input.Disable();
        Time.timeScale = 0;
        
        DeathScore.ChangeScoreText(GameManager.Instance.GetCurrentScore());
        DeathScore.ChangeHighScoreText();
        
        
        DeathCanvas.SetActive(true);
    }
    
    
    public override void UpdateState(UiStateManager ui)
    {
        
    }
    
    public override void ExitState(UiStateManager ui)
    {
        Time.timeScale = 1;
        DeathCanvas.SetActive(false);
    }
}
