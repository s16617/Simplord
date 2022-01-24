using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class TowerAttack : MonoBehaviour
{
    public Enemy enemy;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Tower") && !enemy.isComingBack)
        {
            col.gameObject.GetComponent<Tower>().RemovePoints(enemy.dmgPoints);
            enemy.GoBack();
        }

        if (col.gameObject.CompareTag("TowerSound"))
        {
            col.gameObject.GetComponent<TowerSounds>().MakeSound();
        }

        if (col.gameObject.CompareTag("Trap") && !enemy.isComingBack)
        {
            Destroy(col.gameObject);
            enemy.makeTrapSound();
            enemy.GoBack();
        }
    }
}
