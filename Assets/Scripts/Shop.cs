using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{

    //[SerializeField]
    //public List<Item> items;

    public List<GameObject> itemsUI;

    public Item chosenItem;
    public Item currentItem;

    public GameObject shopPanel;
    public GameObject shopButton;
    public TextMeshProUGUI shopMessage;
    public GameObject textPanel;
    public Image chosenItemImage;

    public Button acceptButton;

    public TextMeshProUGUI quantity;

    public GameObject trapToPlace;

    public bool shopOpen = true;
    public bool shopBlocked = false;

    [SerializeField]
    public Hero mainHero;

    void Start()
    {
        ChooseItem(null);
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
                img.color = new Color32(39, 62, 163, 100);
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
        shopOpen = true;
    }

    public void BlockShop()
    {
        if (chosenItem == null)
        {
            shopButton.SetActive(false);
            shopPanel.SetActive(false);
            textPanel.SetActive(true);
            shopMessage.text = "Shop available only during planning phase.";
            shopOpen = false;
            shopBlocked = true;
        }
    }

    public void UnblockShop()
    {
        if(chosenItem == null)
        {
            textPanel.SetActive(false);
            shopMessage.text = "";
            shopBlocked = false;
            Unhide();
        }
    }

    public void Postpone()
    {
        shopPanel.SetActive(false);
        ChooseItem(null);
        shopButton.SetActive(true);
        shopOpen = false;
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

    public void Hide()
    {
        shopPanel.SetActive(false);
        shopButton.SetActive(false);
        chosenItemImage.gameObject.SetActive(false);
        textPanel.SetActive(false);
        shopMessage.gameObject.SetActive(false);

    }

    public void Unhide()
    {

        if (chosenItem != null)
            chosenItemImage.gameObject.SetActive(true);
        else if (shopOpen)
        {
            shopPanel.SetActive(true);
        }
        else if (!shopBlocked)
            shopButton.SetActive(true);
        else
        {
            textPanel.SetActive(true);
            shopMessage.gameObject.SetActive(true);
        }

    }



}
