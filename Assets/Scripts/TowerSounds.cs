using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSounds : MonoBehaviour
{
    [SerializeField]
    public AudioSource[] hurt;

    public void MakeSound()
    {
        int x  = Random.Range(0, hurt.Length);

        //int x = 0;
        //Debug.Log("react sound:" + x);
        hurt[x].Play();
    }
}
