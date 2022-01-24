using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float time = 0.0f;
    public float interpolationPeriod = 40f;

    public int intensity = 1;

    public GameObject enemyPrefab;

    [SerializeField]
    public GameObject[] towers;
    public GameObject towerChosen;
    private GameObject blockedView;

    [SerializeField]
    public GameObject doorsClosed;

    [SerializeField]
    public GameObject doorsOpen;        

    public bool active = false;

    public bool blocked = false;

    public int floor;

    [SerializeField]
    public AudioSource blockadeSound;

    public Animator animator;

    private void Start()
    {
        blockedView = this.gameObject.transform.Find("BlockedView").gameObject;
        Reset();
    }

    void Update()
    {
        if (active)
        {
            time += (Time.deltaTime * intensity);

            if (time >= interpolationPeriod)
            {
                time -= interpolationPeriod;

                GameObject tmp = Instantiate(enemyPrefab, transform.position, transform.rotation).gameObject;

                int x = Random.Range(0, towers.Length);
                tmp.GetComponent<Enemy>().target = towers[x];
                tmp.GetComponent<Enemy>().floor = floor;
                tmp.GetComponent<Enemy>().spawnpoint = this;
            }
        }
    }

    public void Block()
    {
        blocked = true;
        blockadeSound.Play();
        blockedView.SetActive(true);
    }

    public void Reset()
    {
        blocked = false;
        blockedView.SetActive(false);
    }

    public void ToggleActivation()
    {
        if (active)
        {
            active = false;
            CloseDoors();
        }
        else if (!blocked)
        {
            active = true;
            OpenDoors();
        }
    }

    public void OpenDoors()
    {
        doorsClosed.SetActive(false);
        doorsOpen.SetActive(true);
    }

    public void CloseDoors()
    {
        doorsOpen.SetActive(false);
        doorsClosed.SetActive(true);
        animator.SetTrigger("close");
    }

    public void OpenAndCloseDoors()
    {
        animator.SetTrigger("opening");
    }
}
