using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject pauseMenuPanel; 
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

    public void backToMenuButtonClicked()
    {
        PlayerPrefs.SetInt("SkipNicknameEntry", 1); 
        SceneManager.LoadScene("MainMenuScene");
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
        Time.timeScale = 0f;
        IsGamePaused = true;
    }

    public void ResumeGame()
    {
        pauseMenuPanel.SetActive(false);
        Time.timeScale = 1f; 
        IsGamePaused = false;

    }
}
