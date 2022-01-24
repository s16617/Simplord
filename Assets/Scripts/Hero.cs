using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{
    public float speed = 7.0f;
    public float force = 1f;

    public Animator animator;

    public bool isFacingRight = false;

    private GameObject body;
    public GameObject trapPlace;

    private Enemy inContact;

    private Rigidbody2D rb;

    public bool onStairs = false;
    private bool canGoOnStairs = false;

    private GameObject topStairs;
    private GameObject bottomStairs;

    private bool hasItemToPlace = false;
    private bool nearEnemySpawn = false;

    private SpawnPoint enemySpawn;

    private Item itemToPlace;

    [SerializeField]
    public AudioSource attackSound;

    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.transform.Find("body").gameObject;
        inContact = null;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);

        if (Input.GetKeyDown(KeyCode.Space) && inContact!=null)
        {
            MakeAction();
        }

        if ((Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W)) && (onStairs || canGoOnStairs))
        {
            onStairs = true;
            transform.position = Vector3.MoveTowards(transform.position, topStairs.transform.position,Time.deltaTime * speed);
            moveHorizontal = topStairs.transform.position.x - transform.position.x;
        }
        else if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && (onStairs || canGoOnStairs))
        {
            onStairs = true;
            transform.position = Vector3.MoveTowards(transform.position, bottomStairs.transform.position, Time.deltaTime * speed);
            moveHorizontal = bottomStairs.transform.position.x - transform.position.x;
        }
        else if(!onStairs || (canGoOnStairs && onStairs))
        {
            transform.position += movement * Time.deltaTime * speed;
            onStairs = false;
        }

        if (onStairs)
        {
            float moveVertical = Input.GetAxis("Vertical");
            animator.SetFloat("speed", Mathf.Abs(moveVertical * speed));
        }
        else
        {
            animator.SetFloat("speed", Mathf.Abs(moveHorizontal * speed));
        }

        if ((isFacingRight && moveHorizontal <= 0) || (onStairs && Input.GetAxis("Vertical")==0))
        {
            TurnLeft();
        }
        else if (!isFacingRight && moveHorizontal > 0)
        {
            TurnRight();
        }

        if (hasItemToPlace && Input.GetKeyDown(KeyCode.LeftControl))
        {
            if(itemToPlace.type == ItemType.BLOCKADE && nearEnemySpawn)
            {
                itemToPlace.Use(enemySpawn, null);
            }

            if(itemToPlace.type == ItemType.TRAP)
            {
                itemToPlace.Use(null, trapPlace.transform);
            }

        }
       
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            inContact = col.gameObject.GetComponent<Enemy>();
        }

        if (col.gameObject.CompareTag("BottomStairs") || col.gameObject.CompareTag("TopStairs"))
        {
            topStairs = col.gameObject.GetComponent<Stairs>().topLocation;
            bottomStairs = col.gameObject.GetComponent<Stairs>().bottomLocation;

            canGoOnStairs = true;
        }
        if (col.gameObject.CompareTag("EnemySpawn"))
        {
            nearEnemySpawn = true;
            enemySpawn = col.gameObject.GetComponent<SpawnPoint>();
        }

    }

    public void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            inContact = null;
        }


        if (col.gameObject.CompareTag("BottomStairs"))
        {
            canGoOnStairs = false;
        }

        if (col.gameObject.CompareTag("TopStairs"))
        {
            canGoOnStairs = false;
        }

        if (col.gameObject.CompareTag("EnemySpawn"))
        {
            nearEnemySpawn = false;
        }

    }

    public void MakeAction()
    {
        inContact.Action();
        attackSound.Play();
        inContact = null;
    }

    public void ChangeSpeed(float diff)
    {
        speed *= diff;
    }

    public void ChangeForce(float diff)
    {
        force *= diff;
    }

    public void AddItem(Item itemToAdd)
    {
        itemToPlace = itemToAdd;
        hasItemToPlace = true;
    }

    private void TurnRight()
    {
        isFacingRight = true;
        body.transform.localRotation = Quaternion.Euler(0, 180, 0);
    }

    private void TurnLeft()
    {
        isFacingRight = false;
        body.transform.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void TurningOnStairs(float x_direction)
    {
        if(isFacingRight && x_direction < 0)
            TurnLeft();
        else if(!isFacingRight && x_direction >0)
            TurnRight();
    }
}
