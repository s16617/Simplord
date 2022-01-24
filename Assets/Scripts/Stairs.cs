using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stairs : MonoBehaviour
{
    public GameObject topLocation;

    public GameObject bottomLocation;

    public int bottomFloor;
    public int topFloor;

    public int bottomRoom;
    public int topRoom;

    public bool isBottom;

    public static List<Stairs> bottomStairs = new List<Stairs>();
    public static List<Stairs> topStairs = new List<Stairs>();


    // Start is called before the first frame update
    void Start()
    {
        if (isBottom)
            bottomStairs.Add(this);
        else
            topStairs.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnDestroy()
    {
        if (isBottom)
            bottomStairs.Remove(this);
        else
            topStairs.Remove(this);
    }
}
