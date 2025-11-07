using UnityEngine;

public class PauseMeny : MonoBehaviour
{
    [SerializeField] GameObject pauseMenyUI;
    [SerializeField] bool isPaused;
    [SerializeField] private MonoBehaviour playerMovement;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isPaused = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    private void PauseGame()
    {
        pauseMenyUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;

        if (playerMovement != null)
        {
            playerMovement.enabled = false; 
        }
    }

    private void ResumeGame()
    {
        pauseMenyUI.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;

        if (playerMovement != null)
        {
            playerMovement.enabled = true;
        }
    }

    public void ResumeGameButton() => ResumeGame();

    public void MainMenuButton()
    {
        Debug.Log("Loading Main Menu...");
        Time.timeScale = 1f;
        UnityEngine.SceneManagement.SceneManager.LoadScene("MainMenu");
    }

    public void QuitGameButton()
    {
        Debug.Log("Quitting game...");
        // UnityEditor.EditorApplication.isPlaying = false; // Only works in the editor
        Application.Quit();
    }
}
