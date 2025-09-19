using UnityEngine;

public class UiStateManager : MonoBehaviour
{
    private UiBaseState _currentState;
    
    public UiGameState GameState = new UiGameState();
    public UiPauseState PauseState = new UiPauseState();
    public UiDeathState DeathState = new UiDeathState();
    
    public GameObject uiCanvas;
    public GameObject pauseCanvas;
    public GameObject deathCanvas;
    
    
    void Start()
    {
        _currentState = GameState;
        
        _currentState.EnterState(this);
    }


    void OnUpdate()
    {
        _currentState.UpdateState(this);
    }
    
    void Update()
    {
    }

    void SwitchState(UiBaseState state)
    {
        _currentState = state;
        state.EnterState(this);
    }
    
}
