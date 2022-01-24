using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicCoordinator : MonoBehaviour
{
    [SerializeField]
    public AudioSource win;

    [SerializeField]
    public AudioSource fail;

    [SerializeField]
    public AudioSource fightingPhase;

    [SerializeField]
    public AudioSource planningMusic;

    [SerializeField]
    public AudioSource startingMusic;

    public void WinMusic()
    {
        fightingPhase.Stop();
        win.Play();
    }

    public void FailMusic()
    {
        fightingPhase.Stop();
        fail.Play();
    }

    public void FightingPhaseMusic()
    {
        startingMusic.Stop();
        planningMusic.Stop();
        fightingPhase.Play();
    }

    public void PlanningPhaseMusic()
    {
        fightingPhase.Stop();
        planningMusic.Play();
    }
}
