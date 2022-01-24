using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    public int trustPoints = 100;

    public int currentTrustPoints;

    public int maxTrustPoints;

    [SerializeField]
    public int floor;

    [SerializeField]
    public GameObject TPText;

    public GameState gameState;

    private void Awake()
    {
        currentTrustPoints = trustPoints;
        maxTrustPoints = trustPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TPText.GetComponent<TMPro.TextMeshProUGUI>().text = "" + currentTrustPoints + "/" + maxTrustPoints;
    }

    public void RemovePoints(int points)
    {
        if (points < currentTrustPoints)
        {
            currentTrustPoints -= points;
        }
        else
        {
            currentTrustPoints = 0;
            gameState.Fail();
        }
    }

}
