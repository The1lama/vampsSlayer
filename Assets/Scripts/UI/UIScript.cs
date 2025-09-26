using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class UIScript : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject diedMenuCanvas;
    public GameObject uIMenuCanvas;
    public GameObject levelUpCanvas;
    
    private InputSystem_Actions _input;
    private DeathScore _deathScore;
    private bool _isPaused;


    private void Awake()
    {
        GameManager.Instance.onDeath.AddListener(DeathMenu);
        GameManager.Instance.onLevelUp.AddListener(LevelUpMenu);

    }
    
    private void Start()
    {
        _input = new InputSystem_Actions();
        _input.Enable();
        _input.Player.Pause.performed += PauseFunction;
        
        _deathScore = GetComponent<DeathScore>();
        
    }

    public void GamePause()
    {
        switch (_isPaused)
        {
            case false:
                Time.timeScale = 0;
                _isPaused = true;
                break;
            case true:
                Time.timeScale = 1;
                _isPaused = false;
                break;
        }
    }


    private void DeathMenu()
    {
        
        _deathScore.ChangeScoreText(GameManager.Instance.GetCurrentScore());
        _deathScore.ChangeHighScoreText();
        Time.timeScale = 0;
        
        uIMenuCanvas.SetActive(false);
        diedMenuCanvas.SetActive(true);
    }

    public void LevelUpMenu()
    {
        Debug.Log(levelUpCanvas);
        levelUpCanvas.GetComponentInChildren<UpgradeScript>().ButtonsSet();
        levelUpCanvas.SetActive(true);
        GamePause();
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

    public void PauseScreen()
    {
        GamePause();
        
        if(pauseMenuCanvas != null && !pauseMenuCanvas.activeInHierarchy) 
        {
            pauseMenuCanvas.gameObject.SetActive(true);
        }        
        else if (pauseMenuCanvas != null && pauseMenuCanvas.activeInHierarchy)
        {
            pauseMenuCanvas.gameObject.SetActive(false);
        }
    }
    
    
    private void PauseFunction(InputAction.CallbackContext ctx)
    {
        PauseScreen();
    }
    
}

