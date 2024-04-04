using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public InputField nicknameInputField;
    public GameObject menuButtonsPanel; // Parent GameObject of Play, Options, and Quit buttons
    public GameObject nicknameInputGameObject; // Assign the InputField GameObject in the Inspector
    public GameObject continueButtonGameObject; // Assign the Continue Button GameObject in the Inspector



    public void Start()
    {
        menuButtonsPanel.SetActive(false); // Hide menu buttons initially
    }

    public void OnContinueClicked()
    {
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
