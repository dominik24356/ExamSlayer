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
}
