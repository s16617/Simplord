using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    public ItemType type;

    [SerializeField]
    public Enemy.EnemyType enemyType;

    public GameObject trapToPlace;

    public int quantity = 1;
    public float strength;

    public TextMeshProUGUI descriptionText;

    public TextMeshProUGUI quantityText;

    public Image image;

    public string description = "";
    string imageName = "";

    [SerializeField]
    public AudioSource potionSound;

    void Start()
    {

        SetDescription();
        descriptionText.text = description;
        SetImage();
        SetQuantity();
    }

    void Update()
    {
        
    }

    void SetDescription()
    {
        switch (type)
        {
            case ItemType.POTION_SPEED:
                description = "Speed up your movement.";
                imageName = "PotionSpeed";
                break;
            case ItemType.POTION_ENEMY_SLOW:
                description = "Sharps your senses. Slows enemies.";
                imageName = "PotionTime";
                break;
            case ItemType.BLOCKADE:
                description = "Blocks doors.";
                imageName = "Blockade";
                break;
            case ItemType.TRAP:
                description = "Basic banana trap.";
                imageName = "Trap";
                break;

        }
    }
    void SetImage()
    {
        if (image != null && imageName!="")
        {
            image.sprite = Resources.Load<Sprite>("Items/" + imageName);
        }
    }

    void SetQuantity()
    {
        if (type == ItemType.BLOCKADE || type == ItemType.TRAP)
        {
            quantityText.text = "" + quantity;
            if(quantity == 0)
                quantityText.color = Color.red;
        }
        else
        {
            quantityText.text = "";
        }
    }
    public void Accept(Hero hero)
    {

        switch (type)
        {
            case ItemType.POTION_SPEED:
                hero.ChangeSpeed(1.5f);
                potionSound.Play();
                break;
            case ItemType.POTION_ENEMY_SLOW: 
                Enemy.ChangeSpeed(0.6f);
                potionSound.Play();
                break;
            case ItemType.BLOCKADE:
                hero.AddItem(this);
                break;
            case ItemType.TRAP:
                hero.AddItem(this);
                break;

        }
    }

    public void Use(SpawnPoint spawnPoint, Transform parent) //,Enemy enemy)
    {
        if (quantity > 0)
        {
            switch (type)
            {
                case ItemType.BLOCKADE:
                    if (!spawnPoint.blocked && !spawnPoint.active)
                    {
                        spawnPoint.Block();

                        quantity--;
                        SetQuantity();
                    }
                    break;
                case ItemType.TRAP:

                    Instantiate(trapToPlace, parent.position, parent.rotation);
                    quantity--;
                    SetQuantity();

                    break;
            }
        }
        else
        {
            print("no more items");
        }
    }
}

public enum ItemType
{
    POTION_SPEED,
    POTION_ENEMY_SLOW,
    BLOCKADE,
    TRAP
}
