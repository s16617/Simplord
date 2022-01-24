using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameState : MonoBehaviour
{
    [SerializeField]
    public List<Tower> towers;

    [SerializeField]
    public GameObject[] towersView;

    [SerializeField]
    public TextMeshProUGUI[] towersTPText;

    [SerializeField]
    public List<int> customTowersTP;

    [SerializeField]
    public string sceneToLoad;

    [SerializeField]
    public WavesCoordinator waves;

    public int currentEnemycounter = 0;

    public int enemiesLeft = 0;
    public bool wavesDone = false;

    [SerializeField]
    public GameObject infoPanel;
    public TextMeshProUGUI infoText;
    public GameObject continueButton;

    [SerializeField]
    public GameObject gameMenu;

    [SerializeField]
    public GameObject gameMenuMiddle;

    [SerializeField]
    public GameObject gameMenuOptions;

    [SerializeField]
    public Shop shop;

    [SerializeField]
    public GameObject tutorialPanel;

    private bool levelEnded = false;

    [SerializeField]
    public int levelNr;

    // Start is called before the first frame update
    void Start()
    {
        PlayerData data = SaveSystem.LoadData();
        UIManager.unlockedLevel = data.unlockedLevel;
        UIManager.muted = data.muted;

        if (UIManager.muted)
        {
            UIManager.MuteAllSound();
        }

        if (towers.Count == customTowersTP.Count)
        {
            for(int i=0; i < customTowersTP.Count; i++)
            {
                towers[i].trustPoints = customTowersTP[i];
            }
        }

        foreach(Tower t in towers)
        {
            t.gameState = this;
        }
        waves.gameState = this;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        enemiesLeft = enemies.Length;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            StopGame();
        }

        if (Input.GetKeyUp(KeyCode.Return) && !tutorialPanel.activeSelf)
        {
            waves.Skip();
        }

        //warunek zostanie spełniony gdy wszystkie okresy się zakończą
        //oraz ostatni przeciwnik zostanie pokonany
        if (enemiesLeft==0 && wavesDone && !levelEnded)
        {
            Success();
            levelEnded = true;
        }
    }

    public void Success()
    {
        //wyświetla komunikat o pozytywnym przejściu poziomu
        //oraz wybór "wyjdź" albo "następny poziom"
        PlayerData data = SaveSystem.LoadData();
        int alreadyUnlockedLevel = data.unlockedLevel;

        if(alreadyUnlockedLevel < levelNr + 1)
        {
            UIManager.unlockedLevel = (levelNr + 1);
            SaveSystem.SaveOptions();
        }

        infoText.text = "Success!";
        infoPanel.SetActive(true);
        if(levelNr < 4)
            continueButton.SetActive(true);
        shop.Hide();
        waves.music.WinMusic();
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(sceneToLoad);
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
    public void Fail()
    {
        waves.StopCounting();
        infoText.text = "You failed!";
        infoPanel.SetActive(true);
        continueButton.SetActive(false);
        shop.Hide();
        waves.music.FailMusic();

    }

    public void TryAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        SaveSystem.SaveOptions();
        Application.Quit();
    }

    public void StopGame()
    {
        waves.StopCounting();
        gameMenu.SetActive(true);
        shop.Hide();
        ShowTowersPoints();

        if (tutorialPanel != null)
            tutorialPanel.SetActive(false);
    }

    public void ResumeGame()
    {
        waves.StartCounting();
        gameMenu.SetActive(false);
        shop.Unhide();

        if (tutorialPanel != null && !waves.IsActive() && shop.chosenItem != null)
        {

            tutorialPanel.SetActive(true);
        }
    }

    public void HideOptions()
    {
        gameMenuOptions.SetActive(false);
        SaveSystem.SaveOptions();
        gameMenuMiddle.SetActive(true);
    }

    public void ShowTowersPoints()
    {
        int i = 0;
        foreach (Tower t in towers)
        {
            towersTPText[i].text = "Tower nr" + (i+1) + ": " + t.currentTrustPoints + " / " + t.maxTrustPoints;
            i++;
        }
    }
}

public enum SystemLanguage
{
    Polish,
    English
}
