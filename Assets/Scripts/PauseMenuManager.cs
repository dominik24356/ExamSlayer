using UnityEngine;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; // Assign in Inspector
    public bool IsGamePaused = false;

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (IsGamePaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public bool isGamePausedChecker()
    {
        return this.IsGamePaused;
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void PauseGame()
    {
        pauseMenuPanel.SetActive(true);
        Time.timeScale = 0f; // Pauses the game
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; // Resumes the game
        IsGamePaused = false;
    }
}
