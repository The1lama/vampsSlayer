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
        GameState.UiCanvas = uiCanvas;
        
        _currentState = GameState;
        
        Debug.Log(_currentState);
        _currentState.EnterState(this);
    }


    void OnUpdate()
    {
        _currentState.UpdateState(this);
    }
    
    void Update()
    {
    }

    public void SwitchState(UiBaseState state)
    {
        _currentState = state;

        // GameObject canvas;
        //
        // switch (stateName)
        // {
        //     case "UI":
        //     {
        //         canvas = uiCanvas;
        //         break;
        //     }
        //     case "Pause":
        //     {
        //         canvas = pauseCanvas;
        //         break;
        //     }
        //     case "Death":
        //     {
        //         canvas = deathCanvas;
        //         break;
        //     }
        //     default:
        //     {
        //         canvas = uiCanvas;
        //         break;
        //     }
        // }
        //
        
        state.EnterState(this);
    }
    
}
