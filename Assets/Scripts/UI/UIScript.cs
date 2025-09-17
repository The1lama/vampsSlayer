using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class UIScript : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject diedMenuCanvas;
    public GameObject uIMenuCanvas;
    
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
    
    public void DeathMenu()
    {
        _deathScore.ChangeScoreText(GameManager.Instance.GetCurrentScore());
        Time.timeScale = 0;
        
        
        uIMenuCanvas.SetActive(false);
        diedMenuCanvas.SetActive(true);
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
    
    
    private void PauseFunction(InputAction.CallbackContext ctx)
    {
        PauseFunc();
    }
    
    
    public void PauseFunc()
    {
        if (!_isPaused)
        {
            Time.timeScale = 0;
            _isPaused = true;
            if(pauseMenuCanvas != null) 
            {
                pauseMenuCanvas.gameObject.SetActive(true);
            }
        } 
        else if (_isPaused)
        {
            Time.timeScale = 1;
            _isPaused = false;
            pauseMenuCanvas.gameObject.SetActive(false);
        }
    }
    
}
