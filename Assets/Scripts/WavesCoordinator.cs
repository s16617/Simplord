using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WavesCoordinator : MonoBehaviour
{
    Image timerBar;
    private float currentMaxTime = 5f;

    private float timeLeft;

    private bool active = true;

    private int firstAttackingPhase;

    [SerializeField]
    public MusicCoordinator music;

    [SerializeField]
    public int nrOfWaves;

    [SerializeField]
    public List<float> wavesTimes;

    public GameState gameState;

    [SerializeField]
    public List<SpawnPoint> spawnPoints;

    [SerializeField]
    public GameObject skip;

    [SerializeField]
    public Shop shop;

    [SerializeField]
    public bool activeSpawn = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCounting();
        timerBar = GetComponent<Image>();
        firstAttackingPhase = nrOfWaves - 1;
        NextWave();
    }

    // Update is called once per frame
    void Update()
    {
        if (active)
        {
            if (timeLeft > 0)
            {
                timeLeft -= Time.deltaTime;
                timerBar.fillAmount = timeLeft / currentMaxTime;
            }
            else
            {
                if (nrOfWaves != 0)
                {
                    NextWave();
                }
                else
                {
                    gameState.wavesDone = true;
                    active = false;
                }
                if (activeSpawn)
                {
                    foreach (SpawnPoint sp in spawnPoints)
                    {
                        sp.ToggleActivation();
                    }
                }
            }
        }
    }

    private void NextWave()
    {

        currentMaxTime = wavesTimes[wavesTimes.Count - nrOfWaves];
        timeLeft = currentMaxTime;

        if (nrOfWaves % 2 == 0)
        {
            skip.SetActive(true);
            shop.UnblockShop();
            timerBar.color = new Color32(38, 115, 38, 200);
            if (nrOfWaves < firstAttackingPhase)
            {
                music.PlanningPhaseMusic();
            }
        }
        else
        {
            skip.SetActive(false);
            shop.BlockShop();
            timerBar.color = new Color32(128, 0, 0, 150);
            music.FightingPhaseMusic();
        }
        nrOfWaves--;
    }

    public void Skip()
    {
        if (skip.activeSelf)
            timeLeft = 0;
    }
    public bool IsActive()
    {
        return active;
    }
    public void StopPhase()
    {
        active = false;
    }

    public void StartPhase()
    {
        active = true;
    }

    public void StopCounting()
    {
        Time.timeScale = 0;
    }

    public void StartCounting()
    {
        Time.timeScale = 1f;
    }
}
