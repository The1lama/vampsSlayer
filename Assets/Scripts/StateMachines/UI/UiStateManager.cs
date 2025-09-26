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



    void Awake()
    {
        GameManager.Instance.onDeath.AddListener( () => SwitchState(DeathState));
    }
    
    void Start()
    {
        
        GameState.UiCanvas = uiCanvas;
        PauseState.PauseCanvas = pauseCanvas;
        DeathState.DeathCanvas = deathCanvas;
        
        
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
        
        state.EnterState(this);
    }
    
}
