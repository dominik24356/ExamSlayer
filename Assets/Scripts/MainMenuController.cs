using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public InputField nicknameInputField;
    public GameObject menuButtonsPanel;
    public GameObject nicknamePanel; 
    public GameObject nicknameInputGameObject; 
    public GameObject continueButtonGameObject;

    public GameObject rankingsPanel;
    public Text rankingsText;

    void Start()
{
    if (PlayerPrefs.GetInt("SkipNicknameEntry", 0) == 1)
    {
        this.nicknamePanel.SetActive(false);
        menuButtonsPanel.SetActive(true); 
        nicknameInputGameObject.SetActive(false); 
        continueButtonGameObject.SetActive(false); 
        
        PlayerPrefs.SetInt("SkipNicknameEntry", 0);
        }
        else
        {
            menuButtonsPanel.SetActive(false);

        }
    }


    public void OnContinueClicked()
    {
        this.nicknamePanel.SetActive(false);
        if (!string.IsNullOrWhiteSpace(nicknameInputField.text))
        {
            PlayerPrefs.SetString("PlayerNickname", nicknameInputField.text);

            menuButtonsPanel.SetActive(true);

            nicknameInputGameObject.SetActive(false);
            continueButtonGameObject.SetActive(false);
        }
    }



    public void OnPlayClicked()
    {
        Time.timeScale = 1f; 
        SceneManager.LoadScene("SampleScene");
    }

    public void OnOptionsClicked()
    {
        // TO IMPLEMENT
    }

    public void OnQuitClicked()
    {
        Application.Quit();
    }

    public void ShowRankings()
{
    rankingsPanel.SetActive(true);

    string rankingsDisplay = "Top Scores:\n";
    for (int i = 1; i <= 10; i++)
    {
        if (PlayerPrefs.HasKey($"Score_{i}"))
        {
            int score = PlayerPrefs.GetInt($"Score_{i}");
            string nickname = PlayerPrefs.GetString($"Nickname_{i}", "Player");
            rankingsDisplay += $"{i}. {nickname} - {score}\n";
        }
    }

    rankingsText.text = rankingsDisplay;
    this.menuButtonsPanel.SetActive(false);

    }

    public void backToMenu()
    {
        this.rankingsPanel.SetActive(false);
        this.menuButtonsPanel.SetActive(true);
    }

}
