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
                description = "Ten napój pozwoli Ci poruszać się szybciej.";
                imageName = "PotionSpeed";
                break;
            case ItemType.POTION_ENEMY_SLOW:
                description = "Napój TimeChanger. Wyostrza zmysły. Spowalnia przeciwników.";
                imageName = "PotionTime";
                break;
            case ItemType.POTION_STRENGTH:
                description = "Super siła potrzebna do przesuwania przeciwników? Wypij to!";
                imageName = "PotionStrength";
                break;
            case ItemType.BLOCKADE:
                description = "Zabij drzwi i okna! Nie pozwól im wejść...";
                imageName = "Blockade";
                break;
            case ItemType.TRAP:
                description = "Klasyczna pułapka. Zawsze skuteczna.";
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
                break;
            case ItemType.POTION_ENEMY_SLOW:
                Enemy.ChangeSpeed(0.7f);
                 break;
            case ItemType.POTION_STRENGTH:
                hero.ChangeForce(1.5f);
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
                    if (!spawnPoint.blocked)
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
    POTION_STRENGTH,
    BLOCKADE,
    TRAP
}
