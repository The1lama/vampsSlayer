using UnityEngine;
using UnityEngine.SceneManagement;

public class UiStateManager : MonoBehaviour
{
    private UiBaseState _oldState;
    private UiBaseState _currentState;

    #region Diffrent states
    
        public UiGameState GameState = new UiGameState();
        public UiPauseState PauseState = new UiPauseState();
        public UiDeathState DeathState = new UiDeathState();
        public UiLevelUpState LevelUpState = new UiLevelUpState();
        public UiOptionsState OptionsState = new UiOptionsState();
        
    #endregion

    #region Ui canvases

        public GameObject uiCanvas;
        public GameObject pauseCanvas;
        public GameObject deathCanvas;
        public GameObject levelUpCanvas;
        public GameObject optionsCanvas;
        
    #endregion
    
    private DeathScore _deathScore;
    
    private bool _isPaused;

    void Awake()
    {
        GameManager.Instance.onDead.AddListener( () => SwitchState(DeathState));
        GameManager.Instance.onPause.AddListener( CheckPause );
        GameManager.Instance.onLevelUp.AddListener( () => SwitchState(LevelUpState));
        
        GameManager.Instance.onOptionsMenu.AddListener( () => SwitchState(OptionsState));
        GameManager.Instance.onBackButton.AddListener(() => SwitchState(PauseState));
    }
    
    void Start()
    {

        #region Setup canvas to states

                GameState.UiCanvas = uiCanvas;
                PauseState.PauseCanvas = pauseCanvas;
                DeathState.DeathCanvas = deathCanvas;
                DeathState.DeathScore = GetComponent<DeathScore>();
        
                LevelUpState.LevelUpCanvas = levelUpCanvas;
                OptionsState.OptionsCanvas = optionsCanvas;

        #endregion
        
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
                SwitchState(GameState);
                break;
        }
    }
    
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public void Quit()
    {
        GameManager.Instance.SaveHighScore();
        SceneManager.LoadScene(SceneNames.MainMenu);
        Time.timeScale = 1;
    }

    #region Invoke funcitons

        public void OnBackButtonCall()
        {
            GameManager.Instance.onBackButton.Invoke();
        }
    
        public void OnOptionsMenuCall()
        {
            GameManager.Instance.onOptionsMenu.Invoke();
        }

    #endregion

    
}
