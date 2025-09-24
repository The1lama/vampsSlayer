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
    
    private void Start()
    {
        _input = new InputSystem_Actions();
        _input.Enable();
        _input.Player.Pause.performed += PauseFunction;
        
        _deathScore = GetComponent<DeathScore>();
        
    }

    public void GamePause()
    {
        if (!_isPaused)
        {
            Time.timeScale = 0;
            _isPaused = true;
        } 
        else if (_isPaused)
        {
            Time.timeScale = 1;
            _isPaused = false;
        }
    }
    
    
    public void DeathMenu()
    {
        _deathScore.ChangeScoreText(GameManager.Instance.GetCurrentScore());
        Time.timeScale = 0;
        
        uIMenuCanvas.SetActive(false);
        diedMenuCanvas.SetActive(true);
    }

    public void LevelUpMenu()
    {
        Debug.Log("Level Up Menu");
        Debug.Log(levelUpCanvas);
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
        GameManager.Instance.SaveScore();
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void PauseScreen()
    {
        GamePause();
        
        if(pauseMenuCanvas != null && !pauseMenuCanvas.activeInHierarchy) 
        {
            pauseMenuCanvas.gameObject.SetActive(true);
        }        
        else if (pauseMenuCanvas.activeInHierarchy)
        {
            pauseMenuCanvas.gameObject.SetActive(false);
        }
    }
    
    
    private void PauseFunction(InputAction.CallbackContext ctx)
    {
        PauseScreen();
    }
    
}
