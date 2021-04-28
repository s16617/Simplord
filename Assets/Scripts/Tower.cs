using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Tower : MonoBehaviour
{
    [SerializeField]
    public int trustPoints = 100;

    public int currentTrustPoints;

    [SerializeField]
    public GameObject TPText;

    public GameState gameState;

    private void Awake()
    {
        currentTrustPoints = trustPoints;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        TPText.GetComponent<TMPro.TextMeshProUGUI>().text = "" + currentTrustPoints + "/" + trustPoints;
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
