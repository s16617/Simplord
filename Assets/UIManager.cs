using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public GameObject levels;
    public GameObject mainMenuButtons;

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void OpenLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void GoToLevels()
    {
        mainMenuButtons.SetActive(false);
        levels.SetActive(true);
    }

    public void GoToMainMenu()
    {
        levels.SetActive(false);
        mainMenuButtons.SetActive(true);
    }

    public void Options()
    {

    }

    public void Exit()
    {
        Application.Quit();
    }

}
