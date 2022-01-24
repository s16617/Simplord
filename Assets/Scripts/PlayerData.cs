using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData 
{

    public int unlockedLevel;
    public bool muted;

    public PlayerData()
    {
        muted = UIManager.muted;
        unlockedLevel = UIManager.unlockedLevel;
    }

}
