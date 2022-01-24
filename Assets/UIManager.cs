using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public GameObject levels;
    public GameObject options;
    public GameObject mainMenuButtons;

    public static int unlockedLevel = 1;

    public Button[] levelsButtons;

    public GameObject mutedImage;
    public GameObject unmutedImage;
    public static bool muted = false;

    public TextMeshProUGUI continueText;

    private void Start()
    {
        PlayerData data = SaveSystem.LoadData();
        unlockedLevel = data.unlockedLevel;
        muted = data.muted;

        if (muted)
        {
            MuteAllSound();
        }
        if (continueText != null)
        {
            if (unlockedLevel == 1)        
                continueText.text = "New Game";     
            else         
                continueText.text = "Continue";  
        }
    }

    public void NewGame()
    {
        if (unlockedLevel > 1)
            SceneManager.LoadScene("Level_0" + unlockedLevel);
        else
            SceneManager.LoadScene("Tutorial");
    }

    public void OpenLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void GoToLevels()
    {
        mainMenuButtons.SetActive(false);
        levels.SetActive(true);
        for(int i =0; i<levelsButtons.Length; i++)
        {
            if (i < unlockedLevel)
            {
                levelsButtons[i].interactable = true;
            }
            else
            {
                levelsButtons[i].interactable = false;
            }
        }
    }

    public void GoToMainMenu()
    {
        options.SetActive(false);
        levels.SetActive(false);
        SaveSystem.SaveOptions();
        mainMenuButtons.SetActive(true);
    }

    public void Options()
    {
        mainMenuButtons.SetActive(false);
        options.SetActive(true);
        if(muted)
            mutedImage.SetActive(true);
        else
            unmutedImage.SetActive(true);
    }

    public void Exit()
    {
        SaveSystem.SaveOptions();
        Application.Quit();
    }

    public void ToggleMute()
    {
        if (muted)
        {
            muted = false;
            UnMuteAllSound();
            mutedImage.SetActive(false);
            unmutedImage.SetActive(true);
        }
        else
        {
            muted = true;
            MuteAllSound();
            unmutedImage.SetActive(false);
            mutedImage.SetActive(true);
        }

    }

    public void ResetOptions()
    {
        AudioListener.volume = 1;
        unlockedLevel = 1;
        SaveSystem.SaveOptions();

        if (unlockedLevel == 1 && continueText != null)
        {
            continueText.text = "New Game";
        }
        else
        {
            continueText.text = "Continue";
        }
    }

    public static void MuteAllSound()
    {
        AudioListener.volume = 0;
    }

    public void UnMuteAllSound()
    {
        AudioListener.volume = 1;
    }
}
