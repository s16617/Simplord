using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Enemy;

public class TowerAttack : MonoBehaviour
{
    public Enemy enemy;

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Tower"))
        {
            col.gameObject.GetComponent<Tower>().RemovePoints(enemy.dmgPoints);
            enemy.GoBack();
        }

        if (enemy.type == EnemyType.STRONG && col.gameObject.CompareTag("Exit"))
        {
            Destroy(gameObject);
        }

        if (col.gameObject.CompareTag("Trap"))
        {
            Destroy(col.gameObject);
            enemy.GoBack();
            //Destroy(gameObject);
        }
    }
}
