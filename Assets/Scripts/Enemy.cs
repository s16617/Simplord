using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    public Vector3 pointA;
    public Vector3 pointB;
    public float speed = 1.0f;

    public static float speedChangingPotion = 1.0f;

    public bool canMove = true;

    public int dmgPoints = 10;

    [SerializeField]
    public EnemyType type;

    // Start is called before the first frame update
    void Start()
    {
        pointA = transform.position;
        pointB = target.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, pointB, Time.deltaTime * speed * speedChangingPotion);
        }

        if(transform.position == pointA && pointA == pointB)
        {
            Destroy(gameObject);
        }
    }
    public void GoBack()
    {
        pointB = pointA;
    }

    public void Action()
    {
        switch (type)
        {
            case EnemyType.FLOWERGUY:
                GoBack();
                break;
            case EnemyType.STRONG:
                //canMove = false;
                break;

        }
    }

    public static void ChangeSpeed(float diff)
    {
        speedChangingPotion *= diff;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (type == EnemyType.STRONG)
        {
            Destroy(gameObject);
        }
    }

    public enum EnemyType
    {
        FLOWERGUY,
        STRONG,
        GENERIC
    }
}
