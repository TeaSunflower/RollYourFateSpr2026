using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

enum State
{
    Command,
    Item,
    Results,
    Lose
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

    string menuPrompt = "Actions:" +
                "\nPrompt - Display encounter description" +
                "\nInventory - Display inventory" +
                "\nUse - Use an item";

    string command;

    int lvlComplete;

    bool lose;
    bool itemUsed;
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
        lose = false;
        displayPrinted = false;

        // Hardcoded starter set of items
        starterSet = new List<Item>();
        starterSet.Add(new Item("Short Sword", 3, 1, 0));
        starterSet.Add(new Item("Buckler", 0, 3, 1));
        starterSet.Add(new Item("Magic Twig", 1, 0, 3));

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

                itemUsed = false;
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
                        itemUsed = true;
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
                        displayText.text += "GAME OVER\n\nEncounters Completed: " + lvlComplete;
                        displayPrinted = true;
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
