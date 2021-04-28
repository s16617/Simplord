using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    [SerializeField]
    public List<Item> items;

    public List<GameObject> itemsUI;

    public Item chosenItem;
    public Item currentItem;

    public GameObject shopPanel;
    public GameObject shopButton;
    public Image chosenItemImage;

    public Button acceptButton;

    public TextMeshProUGUI quantity;

    public GameObject trapToPlace;

    [SerializeField]
    public Hero mainHero;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void ChooseItem(GameObject parentItem)
    {
        currentItem = null;
        foreach(GameObject itemUI in itemsUI)
        {
            if (parentItem == itemUI)
            {
                Image img = itemUI.GetComponent<Image>();
                img.color = UnityEngine.Color.red;
                currentItem = itemUI.GetComponent<Item>();
            }
            else {
                Image img = itemUI.GetComponent<Image>();
                img.color = new Color32(0, 210, 255, 100);
            }
        }
        if (currentItem != null)
            acceptButton.interactable = true;
        else
            acceptButton.interactable = false;
    }

    public void OpenShop()
    {
        shopButton.SetActive(false);
        shopPanel.SetActive(true);
    }

    public void Postpone()
    {
        shopPanel.SetActive(false);
        ChooseItem(null);
        shopButton.SetActive(true);
    }

    public void Accept()
    {
        chosenItem = currentItem;
        chosenItemImage.sprite = chosenItem.image.sprite;
        chosenItemImage.gameObject.SetActive(true);
        if (chosenItem.quantity > 1)
        {
            quantity.text = "" + chosenItem.quantity;
        }
        else
        {
            quantity.text = "";
        }
        chosenItem.quantityText = quantity;
        chosenItem.Accept(mainHero);
        chosenItem.trapToPlace = trapToPlace;
        shopPanel.SetActive(false);

    }



}
