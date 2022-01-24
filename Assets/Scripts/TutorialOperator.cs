using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TutorialOperator : MonoBehaviour
{
    [SerializeField]
    public GameState gameState;

    [SerializeField]
    public Hero hero;

    [SerializeField]
    public TextMeshProUGUI tutorialInstruction;

    [SerializeField]
    public GameObject tutorialPanel;

    [SerializeField]
    public GameObject ShopCanvas;

    [SerializeField]
    public Button nextButton;

    private bool waitingForAction = true;
    private bool timerActive = true;

    private bool up_buttonPressed = false;
    private bool down_buttonPressed = false;
    private bool left_buttonPressed = false;
    private bool right_buttonPressed = false;

    private bool action_Performed = false;

    private TutorialStages[] order = { 
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.WALKING,
        TutorialStages.DIALOUGUE,
        TutorialStages.STAIRS,
        TutorialStages.CAMERA,
        TutorialStages.CAMERA,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.SHOP,
        TutorialStages.ITEMS,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.DIALOUGUE,
        TutorialStages.PLAY,
        TutorialStages.NONE
        };

    private string[] messages = {
        "Welcome to Simplord!  \n" +
            "Press \"Next\" or Enter key to see next step.",
        "This game is taking place in the main hero imagination. These instructions will help you learn how to control him in this strange fantasy.",
        "To walk use either left/right arrow keys or A/D keys. Walk around to enable the next step.",
        "Now he knows how to walk, but sometimes the hero needs to go to other floors.",
        "To use the stairs between the floors hold either up/down arrow keys or W/S keys near the stairs. Go up and down the stairs to enable the next step.", //stairs
        "Very good! He seems to know how to walk around know. The next will be observation. To see whole building press \"m\".", //camera
        "Good job! \n Now go back to the previous view by pressing \"m\" once again.", //camera
        "There is the tower on the second floor - the lovely girl sitting by her computer, that hero wants to protect from all these guys.", //dialogue
        "The points above her head indicate how she is feeling. If you let her lose all her trust points, you will fail.", //dialogue
        "The enemies with flowers that will come out from the door on the right are taking 10 of her trust points.", //dialogue
        "The enemies that will come out from the window on the left are stalkers. They are quick and take 15 of girl's trust points.", //dialogue
        "You will have a quick chance to fight them at the end of this tutorial.", //dialogue
        "To attack an enemy you will have to get close to him and send him home by pressing Space key.", //dialogue
        "You can also use items. The items are in the shop. For this tutorial there will be only traps available, but there are potions and blockades too.", //dialogue
        "Use the shop by choosing the item (clicking on the item icon) and clicking the accept button. You cannot change the item after accepting it.", //dialogue
        ">empty<", //shop
        "When you have the items, they will appear in the left corner with the number of them available. Traps are reacting with the enemy in the place where they are placed. To use an item press left Ctrl.", //items  
        "There are two phases. The first is planning phase. You will be able to buy items in the shop and look around without worries. The timer above will be green. You can skip this phase by pressing the \"Skip\" button or Enter key.", //dialogue
        "Enemies will come out during attacking phase. The shop will be unavailable if you did not choose any item before that. The timer will be red during that phase.", //dialogue
        "I think the hero is ready now. To see all the instructions, open game menu by pressing \"Esc\" key and go to Options, everything should be there.", //dialogue
        "Now try to save the girl from all the other guys! Good luck!", //play
        "" //none
        };

    int currentAction = 0;

    // Update is called once per frame
    void Update()
    {
        if (timerActive && waitingForAction)
        {
            gameState.waves.StopPhase();
            gameState.waves.skip.gameObject.SetActive(false);
            ShopCanvas.SetActive(false);
            timerActive = false;
        }else if(!timerActive && !waitingForAction)
        {
            gameState.waves.StartPhase();
            gameState.waves.skip.gameObject.SetActive(true);

            timerActive = true;
        }

        if (nextButton.IsActive() && Input.GetKeyUp(KeyCode.Return))
        {
            NextInstruction();
        }

        tutorialInstruction.text = messages[currentAction];

        switch (order[currentAction])
        {
            case TutorialStages.WALKING:
                {
                    float moveHorizontal = Input.GetAxis("Horizontal");

                    if (moveHorizontal > 0)
                        right_buttonPressed = true;
                    else if (moveHorizontal < 0)
                        left_buttonPressed = true;
                    if (left_buttonPressed && right_buttonPressed)
                    {
                        nextButton.gameObject.SetActive(true);
                    }
                    break;
                }
            case TutorialStages.STAIRS:
                {
                    float moveVertical = Input.GetAxis("Vertical");

                    if (moveVertical > 0 && hero.onStairs)
                        up_buttonPressed = true;
                    else if (moveVertical < 0 && hero.onStairs)
                        down_buttonPressed = true;
                    if (up_buttonPressed && down_buttonPressed)
                    {
                        nextButton.gameObject.SetActive(true);
                    }
                    break;
                }
            case TutorialStages.DIALOUGUE:
                {
                    nextButton.gameObject.SetActive(true);
                    break;
                }
            case TutorialStages.CAMERA:
                {
                    if (Input.GetKeyUp(KeyCode.M))
                    {
                        NextInstruction();
                    }
                    break;
                }
            case TutorialStages.ITEMS:
                {
                    if (Input.GetKeyUp(KeyCode.LeftControl))
                        action_Performed = true;
                    if (action_Performed)
                    {
                        nextButton.gameObject.SetActive(true);
                    }
                    break;
                }
            case TutorialStages.SHOP:
                {
                    ShopCanvas.SetActive(true);
                    tutorialPanel.SetActive(false);
                    break;
                }
            case TutorialStages.PLAY:
                {
                    waitingForAction = false;
                    nextButton.gameObject.SetActive(true);
                    break;
                }
        }
        
    }

    public void NextInstruction()
    {
        if (order[currentAction] != TutorialStages.NONE)
        {
            tutorialPanel.SetActive(true);
            currentAction++;
            nextButton.gameObject.SetActive(false);
        }
        
        if(order[currentAction] == TutorialStages.NONE)
        {
            tutorialPanel.SetActive(false);
        }
    }

    public enum TutorialStages
    {
        WALKING,
        STAIRS,
        SHOP,
        ITEMS,
        CAMERA,
        DIALOUGUE,
        PLAY,
        NONE
    }
}
