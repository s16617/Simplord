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
    private GameObject stairsBody;
    public GameObject trapPlace;

    private Enemy inContact;

    private Rigidbody2D rb;

    private bool onStairs = false;
    private bool goingUp = false;
    private bool goingDown = false;

    private bool hasItemToPlace = false;
    private bool nearEnemySpawn = false;

    private SpawnPoint enemySpawn;

    private Item itemToPlace;

    // Start is called before the first frame update
    void Start()
    {
        body = this.gameObject.transform.Find("body").gameObject;
        stairsBody = this.gameObject.transform.Find("stairsBody").gameObject;
        inContact = null;

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");

        Vector3 movement = new Vector3(moveHorizontal, 0f, 0f);

        animator.SetFloat("speed", Mathf.Abs(moveHorizontal * speed));

        if (isFacingRight && moveHorizontal < 0)
        {
            isFacingRight = false;
            body.transform.localRotation = Quaternion.Euler(0, 0, 0);
        }

        if (!isFacingRight && moveHorizontal > 0)
        {
            isFacingRight = true;
            body.transform.localRotation = Quaternion.Euler(0, 180, 0);
        }

        if (Input.GetKeyDown(KeyCode.Q) && inContact!=null)
        {
            MakeAction();
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) )
        {
            stairsBody.SetActive(true);
        }

        if (onStairs)
        {
            rb.gravityScale = 0;
        }
        else
        {
            rb.gravityScale = 0;
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

        transform.position += movement * Time.deltaTime * speed;

       
    }

    public void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            inContact = col.gameObject.GetComponent<Enemy>();
        }

        if (col.gameObject.CompareTag("Stairs"))
        {
           
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

        if (col.gameObject.CompareTag("Stairs"))
        {

        }

        if (col.gameObject.CompareTag("EnemySpawn"))
        {
            nearEnemySpawn = false;
        }

    }

    public void MakeAction()
    {
        inContact.Action();
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
}
