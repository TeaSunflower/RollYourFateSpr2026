using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

enum State
{
    Start,
    Command,
    Item,
    Results
}

public class GameLoop : MonoBehaviour
{
    [SerializeField]
    InputField input;

    [SerializeField]
    Text displayText;

    [SerializeField]
    SpriteRenderer knightSprite;

    [SerializeField]
    SpriteRenderer squireSprite;

    [SerializeField]
    SpriteRenderer enemySprite;

    [SerializeField]
    Sprite enemyDefeated;

    [SerializeField]
    Sprite enemyAttack;

    [SerializeField]
    Sprite enemyBlock;

    [SerializeField]
    Sprite enemyMagic;

    [SerializeField]
    Sprite knightStart;

    [SerializeField]
    Sprite knightIdle;

    [SerializeField]
    Sprite knightAttack;

    [SerializeField]
    Sprite knightBlock;

    [SerializeField]
    Sprite knightMagic;

    [SerializeField]
    Sprite knightDefeated;

    [SerializeField]
    Sprite squireStart;

    [SerializeField]
    Sprite squireIdle;

    [SerializeField]
    Sprite squireDefeated;

    [SerializeField]
    Sprite squireAttack;

    [SerializeField]
    Sprite squireBlock;

    [SerializeField]
    Sprite squireMagic;

    string menuPrompt = "Actions:" +
                "\nPrompt - Display encounter description" +
                "\nInventory - Display inventory" +
                "\nUse - Use an item";

    string command;

    int lvlComplete;

    bool displayPrinted;

    List<Item> starterSet;

    State state;

    Item currentItem;
    Inventory inventory;
    EventList eventPool;
    Event currentEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        command = "null";

        displayText.text = "";

        lvlComplete = 0;
        displayPrinted = false;

        // Hardcoded starter set of items
        starterSet = new List<Item>();
        starterSet.Add(new Item("Short Sword", 3, 0, 0));
        starterSet.Add(new Item("Buckler", 0, 3, 0));
        starterSet.Add(new Item("Magic Twig", 0, 0, 3));

        currentItem = new Item("null", 0, 0, 0);

        inventory = new Inventory(starterSet);
        eventPool = new EventList();

        currentEvent = eventPool.GetRandomEvent();
    }

    // Update is called once per frame
    void Update()
    {
        switch(state)
        {
            case State.Start:
                if (!displayPrinted)
                {
                    displayText.text = "Welcome, squire and knight. The two of you are about embark on a grand adventure (of no foreseeable end) " +
                        "and this epic tale will surely accredit to correct person responsible for its success (it won't). Simply type the choice word for word " +
                        "to select your action, hit the Enter key to submit, and your trusted knight will perform said action and then clumsly break whatever item you gave him to use. " +
                        "Good luck, fair travelers, and hopefully you make it further than the starting line!" + "\n\nType anything to continue...";
                    displayPrinted = true;
                }

                if(command != "null")
                {
                    displayText.text = "";
                    input.text = "";
                    command = "null";
                    displayPrinted = false;
                    state = State.Command;
                }
                break;
            
            case State.Command:
                // Set sprites based on event
                if (currentEvent.RequiredStats.Damage < currentEvent.RequiredStats.Block && currentEvent.RequiredStats.Damage < currentEvent.RequiredStats.Magic)
                {
                    enemySprite.sprite = enemyAttack;
                }
                else if (currentEvent.RequiredStats.Block < currentEvent.RequiredStats.Magic)
                {
                    enemySprite.sprite = enemyBlock;
                }
                else
                {
                    enemySprite.sprite = enemyMagic;
                }

                knightSprite.sprite = knightIdle;
                squireSprite.sprite = squireIdle;

                if(!displayPrinted && displayText.text == "")
                {
                    displayText.text = currentEvent.Description + "\n\n" + menuPrompt;
                    displayPrinted = true;
                }
                else
                {
                    if(!displayPrinted)
                    {
                        displayText.text += "\n\n" + menuPrompt;
                        displayPrinted = true;
                    }
                }

                if (command != "null")
                {
                    displayText.text = "";
                    switch (command.ToLower())
                    {
                        case "prompt":
                            displayText.text = currentEvent.Description;
                            break;
                        
                        case "inventory":
                            displayText.text += inventory.ReturnItemList();
                            break;

                        case "use":
                            state = State.Item;
                            break;

                        default:
                            displayText.text += "Command not recognized, try again.\n\n";
                            break;
                    }
                    input.text = "";
                    command = "null";
                    displayPrinted = false;
                }
                    break;

            case State.Item:
                
                if (!displayPrinted && displayText.text == "")
                {
                    displayText.text = "What item would you like to use?\n\n" + inventory.ReturnItemList() + "\n\nBack - Return to Action Menu";

                    displayPrinted = true;
                }
                else
                {
                    if (!displayPrinted)
                    {
                        displayText.text += "\n\nWhat item would you like to use?\n\n" + inventory.ReturnItemList() + "\n\nBack - Return to Action Menu";
                        displayPrinted = true;
                    }
                }
                
                if (command.ToLower() == "back")
                {
                    displayText.text = "";
                    state = State.Command;
                    input.text = "";
                    command = "null";
                    displayPrinted = false;
                }
                
                if(command != "null")
                {
                    displayText.text = "";
                    currentItem = inventory.SearchItem(command);
                    if (currentItem.Name == "null")
                    {
                        displayText.text = "You don't seem to have this item.";
                    }
                    else
                    {
                        displayText.text = "You used " + currentItem.Name + "\n\n";
                        inventory.RemoveItem(currentItem);
                        state = State.Results;
                    }
                    input.text = "";
                    command = "null";
                    displayPrinted = false;
                }

                
                break;

            case State.Results:
                if (!displayPrinted)
                {
                    if (currentItem.Damage >= currentEvent.RequiredStats.Damage || currentItem.Block >= currentEvent.RequiredStats.Block || currentItem.Magic >= currentEvent.RequiredStats.Magic)
                    {
                        // Set sprites
                        enemySprite.sprite = enemyDefeated;

                        if (currentItem.Damage > currentItem.Block && currentItem.Damage > currentItem.Magic)
                        {
                            squireSprite.sprite = squireAttack;
                            knightSprite.sprite = knightAttack;
                        }
                        else if (currentItem.Block > currentItem.Magic)
                        {
                            squireSprite.sprite = squireBlock;
                            knightSprite.sprite = knightBlock;
                        }
                        else
                        {
                            squireSprite.sprite = squireMagic;
                            knightSprite.sprite = knightMagic;
                        }

                        displayText.text += currentEvent.WinText + "\n\n";
                        Item[] rewards = currentEvent.GetRewardItems();
                        for (int i = 0; i < rewards.Count(); i++)
                        {
                            displayText.text += "Obtained " + rewards[i].Name + "!\n";
                            inventory.AddItem(rewards[i]);
                        }
                        lvlComplete++;

                        displayText.text += "\nType anything to continue...";
                        displayPrinted = true;
                    }
                    else
                    {
                        displayText.text += "GAME OVER\n\nEncounters Completed: " + lvlComplete + "\n\nType anything to restart...";
                        displayPrinted = true;

                        currentItem = new Item("null", 0, 0, 0);
                        inventory = new Inventory(starterSet);
                        lvlComplete = 0;

                        knightSprite.sprite = knightDefeated;
                        squireSprite.sprite = squireDefeated;
                    }
                }

                if (command != "null")
                {
                    displayText.text = "";
                    currentEvent = eventPool.GetRandomEvent();
                    state = State.Command;
                    input.text = "";
                    command = "null";
                    displayPrinted = false;
                }
                break;

            default:
                break;
        }
    }

    public void GetInputField()
    {
        command = input.text;
    }
}
