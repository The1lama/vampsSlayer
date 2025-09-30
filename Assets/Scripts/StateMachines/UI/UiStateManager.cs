using UnityEngine;

public class UiStateManager : MonoBehaviour
{
    private UiBaseState _oldState;
    
    private UiBaseState _currentState;
    
    public UiGameState GameState = new UiGameState();
    public UiPauseState PauseState = new UiPauseState();
    public UiDeathState DeathState = new UiDeathState();
    public UiLevelUpState LevelUpState = new UiLevelUpState();
    
    public GameObject uiCanvas;
    public GameObject pauseCanvas;
    public GameObject deathCanvas;
    public GameObject levelUpCanvas;

    private DeathScore _deathScore;
    
    private bool _isPaused;

    void Awake()
    {
        GameManager.Instance.onDeath.AddListener( () => SwitchState(DeathState));
        GameManager.Instance.onPause.AddListener( CheckPause );
        GameManager.Instance.onLevelUp.AddListener( () => SwitchState(LevelUpState));
    }
    
    void Start()
    {
        
        GameState.UiCanvas = uiCanvas;
        
        PauseState.PauseCanvas = pauseCanvas;
        
        DeathState.DeathCanvas = deathCanvas;
        DeathState.DeathScore = GetComponent<DeathScore>();

        LevelUpState.LevelUpCanvas = levelUpCanvas;
        
        _currentState = GameState;
        
        _currentState.EnterState(this);
    }


    void OnUpdate()
    {
        _currentState.UpdateState(this);
    }
    

    public void SwitchState(UiBaseState state)
    {
        _oldState = _currentState;
        _currentState.ExitState(this);
        
        _currentState = state;
        state.EnterState(this);
        Debug.Log(Time.timeScale);
    }

    private void CheckPause()
    {
        switch (_isPaused)
        {
            case false:
                _isPaused = true;
                SwitchState(PauseState);
                break;
            
            case true:
                _isPaused = false;
                SwitchState(_oldState);
                break;
        }
    }
}
