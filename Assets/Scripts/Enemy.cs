using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    public GameObject target;
    public Vector3 spawnPosition;
    public Vector3 currentGoalPosition;
    public Vector3 goalPosition;
    public float speed = 1.0f;
    public SpawnPoint spawnpoint;

    public static float speedChangingPotion = 1.0f;

    public bool canMove = true;

    public int dmgPoints = 10;

    public bool isFacingRight = false;

    private GameObject body;

    SpriteRenderer sprite;

    public int floor;
    private int startFloor;

    private bool isOnStairs;
    public bool isComingBack;

    [SerializeField]
    public EnemyType type;

    [SerializeField]
    public AudioSource enemyTrapSound;

    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.transform.Find("body").gameObject;
        sprite = body.GetComponent<SpriteRenderer>();

        spawnPosition = transform.position;
        goalPosition = target.transform.position;
        currentGoalPosition = goalPosition;
        startFloor = floor;
        isOnStairs = false;
        isComingBack = false;

        if (type == EnemyType.STALKER)
        {
            dmgPoints = 15;
            speed = 5.5f;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if ((target.GetComponent<Tower>().floor > floor && goalPosition != spawnPosition) || (startFloor > floor && goalPosition == spawnPosition))
        {

            if (currentGoalPosition == transform.position && isOnStairs)
            {
                floor++;
                isOnStairs = false;
                if (target.GetComponent<Tower>().floor == floor)
                {
                    currentGoalPosition = target.gameObject.transform.position;
                }
                else
                {
                    currentGoalPosition = Stairs.bottomStairs.Find(x => x.bottomFloor == floor).gameObject.transform.position;
                }
            }
            else if (currentGoalPosition.y != transform.position.y && !isOnStairs)
            {
                /*
                List<Stairs> results = Stairs.bottomStairs.FindAll(x => x.bottomFloor == floor);
                Debug.Log(results.Count);
                int y = Random.Range(0, results.Count);
                Debug.Log(y);
                currentGoalPosition = (results.ToArray()[y]).gameObject.transform.position;
                */
                currentGoalPosition = Stairs.bottomStairs.Find(x => x.bottomFloor == floor).gameObject.transform.position;
            }
            else if ((currentGoalPosition == transform.position && !isOnStairs) || (goalPosition == spawnPosition && isOnStairs))
            {
                currentGoalPosition = Stairs.topStairs.Find(x => x.bottomFloor == floor).gameObject.transform.position;
                isOnStairs = true;
            }

        }
        else if ((target.GetComponent<Tower>().floor < floor && goalPosition != spawnPosition) || (startFloor < floor && goalPosition == spawnPosition))
        {
            if (currentGoalPosition == transform.position && isOnStairs)
            {
                isOnStairs = false;
                if (target.GetComponent<Tower>().floor == floor)
                {
                    currentGoalPosition = target.gameObject.transform.position;
                }
                else
                {
                    currentGoalPosition = Stairs.topStairs.Find(x => x.topFloor == floor).gameObject.transform.position;
                    
                }
            }
            else if (currentGoalPosition.y != transform.position.y && !isOnStairs)
            {
                currentGoalPosition = Stairs.topStairs.Find(x => x.topFloor == floor).gameObject.transform.position;
            }

            else if (currentGoalPosition == transform.position && !isOnStairs)
            {
                currentGoalPosition = Stairs.bottomStairs.Find(x => x.topFloor == floor).gameObject.transform.position;
                isOnStairs = true;
                floor--;
            }
            else if (currentGoalPosition != transform.position && isOnStairs)
            {
                currentGoalPosition = Stairs.bottomStairs.Find(x => x.bottomFloor == floor).gameObject.transform.position;
            }

        }
        else if ((target.GetComponent<Tower>().floor == floor && goalPosition != spawnPosition) && !isOnStairs)
        {
            currentGoalPosition = target.gameObject.transform.position;
        }
        else if ((target.GetComponent<Tower>().floor == floor && goalPosition != spawnPosition) && isOnStairs && currentGoalPosition == transform.position)
        {
            isOnStairs = false;
            currentGoalPosition = target.gameObject.transform.position;
        }
        else if (startFloor == floor && goalPosition == spawnPosition && !isOnStairs)
        {
            currentGoalPosition = spawnPosition;
        }
        else if (startFloor == floor && goalPosition == spawnPosition && isOnStairs && currentGoalPosition != transform.position)
        {
            currentGoalPosition = Stairs.bottomStairs.Find(x => x.topFloor == (floor + 1)).gameObject.transform.position;
        }
        else if (goalPosition == spawnPosition && isOnStairs && currentGoalPosition == transform.position)
        {
            isOnStairs = false;
            if (startFloor == floor)
            {
                currentGoalPosition = spawnPosition;
            }
        }


        if (canMove)
        {
            transform.position = Vector3.MoveTowards(transform.position, currentGoalPosition, Time.deltaTime * speed * speedChangingPotion);
        }

        if (transform.position == spawnPosition && spawnPosition == goalPosition)
        {
            spawnpoint.OpenAndCloseDoors();
            Destroy(gameObject);
        }

        float moveDirection = transform.position.x - currentGoalPosition.x;

        if (isFacingRight && moveDirection < 0)
        {
            isFacingRight = false;
            body.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (!isFacingRight && moveDirection > 0)
        {
            isFacingRight = true;
            body.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }
    }
    public void GoBack()
    {
        goalPosition = spawnPosition;
        isComingBack = true;
        sprite.color = new Color(0.7f, 0.7f, 0.7f, 1);

    }

    public void Action()
    {
        switch (type)
        {
            case EnemyType.FLOWERGUY:
                GoBack();
                break;
            case EnemyType.STALKER:
                GoBack();
                break;

        }
    }

    public static void ChangeSpeed(float diff)
    {
        speedChangingPotion *= diff;
    }

    public void makeTrapSound()
    {
        enemyTrapSound.Play();
    }

    public enum EnemyType
    {
        FLOWERGUY,
        STALKER,
        GENERIC
    }
}
