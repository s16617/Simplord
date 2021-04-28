using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    private float time = 0.0f;
    public float interpolationPeriod = 40f;

    public int intensity = 1;

    public GameObject enemyPrefab;

    public GameObject towerChosen;
    private GameObject blockedView;

    public bool active = false;

    public bool blocked = false;

    private void Start()
    {
        blockedView = this.gameObject.transform.Find("BlockedView").gameObject;
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
                tmp.GetComponent<Enemy>().target = towerChosen;
            }
        }
    }

    public void Block()
    {
        blocked = true;
        blockedView.SetActive(true);
    }

    public void ToggleActivation()
    {
        if (active)
        {
            active = false;
        }
        else if (!blocked)
        {
            active = true;
        }
    }

}
