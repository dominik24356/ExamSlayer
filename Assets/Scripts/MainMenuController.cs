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
    public GameObject optionsPanel;
    public Button toggleSoundButton;

    public GameObject rankingsPanel;
    public Text rankingsText;

    private bool isSoundOn = true;

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

    public void SetDifficulty(string difficulty)
    {
        Debug.Log("Difficulty set to: " + difficulty);
    }

    public void OnToggleSound()
    {
        isSoundOn = !isSoundOn;
        AudioListener.volume = isSoundOn ? 1.0f : 0.0f;
        UpdateSoundButtonText();
    }

    void UpdateSoundButtonText()
    {
        toggleSoundButton.GetComponentInChildren<Text>().text = isSoundOn ? "TURN OFF SOUND" : "TURN ON SOUND";
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
        optionsPanel.SetActive(true);
        menuButtonsPanel.SetActive(false);
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
        this.optionsPanel.SetActive(false);
        this.rankingsPanel.SetActive(false);
        this.menuButtonsPanel.SetActive(true);
    }

}
