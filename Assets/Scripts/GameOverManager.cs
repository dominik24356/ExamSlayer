using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverManager : MonoBehaviour
{
    public GameObject gameOverPanel; 

    private void Start()
    {
        gameOverPanel.SetActive(false);
    }

    private void Update()
    {
        if (gameOverPanel.activeInHierarchy && Input.GetKeyDown(KeyCode.Return))
        {
            ContinueGame();
        }
    }

    public void ShowGameOver()
    {
        gameOverPanel.SetActive(true);
    }

    void ContinueGame()
    {
        gameOverPanel.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        FindObjectOfType<GameManager>().setLivesAndScore();


    }
}
