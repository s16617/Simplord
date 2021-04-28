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
    public List<GameObject> towersView;

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

    [SerializeField]
    public GameObject gameMenu;

    private bool levelEnded = false;

    // Start is called before the first frame update
    void Start()
    {
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
        infoText.text = "Sukces!";
        infoPanel.SetActive(true);
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
        infoText.text = "Porażka!";
        infoPanel.SetActive(true);
        print("failed");
        //wyświetlić menu z "powtórz" albo "zakończ"

    }

    public void TryAgain()
    {
        Scene scene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(scene.name);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StopGame()
    {
        waves.StopCounting();
        gameMenu.SetActive(true);
    }

    public void ResumeGame()
    {
        waves.StartCounting();
        gameMenu.SetActive(false);
    }
}

public enum SystemLanguage
{
    Polish,
    English
}
