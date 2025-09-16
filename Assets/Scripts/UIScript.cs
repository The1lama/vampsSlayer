using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;



public class UIScript : MonoBehaviour
{
    public GameObject pauseMenuCanvas;
    public GameObject diedMenuCanvas;
    public GameObject uiElement;
    
    private InputSystem_Actions _input;
    private bool _isPaused;
    
    private void Start()
    {
        _input = new InputSystem_Actions();
        _input.Enable();
        _input.Player.Pause.performed += PauseFunction;

    }
    
    public void DeathMenu()
    {
        Time.timeScale = 0;
        uiElement.SetActive(false);
        
        diedMenuCanvas.SetActive(true);
    }
    

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1;
    }
    

    public void Quit()
    {
        GameManager.gameManager.SaveScore();
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
            pauseMenuCanvas.gameObject.SetActive(true);
        }
        else if(_isPaused)
        {
            Time.timeScale = 1;
            _isPaused = false;
            pauseMenuCanvas.gameObject.SetActive(false);
        }
    }
    
}
