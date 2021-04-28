using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuCoordinator : MonoBehaviour
{
    public GameObject levels;
    public GameObject mainMenuButtons;

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

    public void LoadScene()
    {

    }
}
