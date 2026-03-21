using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameLoop : MonoBehaviour
{
    string menuPrompt = "Actions:" +
                "\nInventory - Display Inventory" +
                "\nUse - Use an item";

    string command;
    string currentItemName;

    int lvlComplete;

    bool lose;
    bool itemUsed;

    List<Item> starterSet;

    Item currentItem;
    Inventory inventory;
    EventList eventPool;
    Event currentEvent;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lvlComplete = 0;
        lose = false;

        starterSet = new List<Item>();
        starterSet.Add(new Item("Short Sword", 3, 1, 0));
        starterSet.Add(new Item("Buckler", 0, 3, 1));
        starterSet.Add(new Item("Magic Twig", 1, 0, 3));

        currentItem = new Item("null", 0, 0, 0);
        inventory = new Inventory(starterSet);
        eventPool = new EventList();
    }

    // Update is called once per frame
    void Update()
    {
        while (!lose)
        {
            currentEvent = eventPool.GetRandomEvent();
            itemUsed = false;

            Console.WriteLine(currentEvent.Description);
            Console.WriteLine();

            Console.WriteLine(menuPrompt);
            Console.WriteLine();

            while (!itemUsed)
            {
                Console.Write("Command: ");
                command = Console.ReadLine()!;
                Console.WriteLine();

                switch (command.ToLower())
                {
                    case "inventory":
                        inventory.ReturnItemList();
                        Console.WriteLine();
                        break;

                    case "use":
                        while (!itemUsed)
                        {
                            Console.WriteLine("What item would you like to use?");
                            Console.WriteLine();
                            currentItemName = Console.ReadLine()!;
                            Console.WriteLine();
                            currentItem = inventory.SearchItem(currentItemName);
                            if (currentItem.Name == "null")
                            {
                                Console.WriteLine("You don't seem to have this item. Try something else?");
                                Console.WriteLine();
                            }
                            else
                            {
                                Console.WriteLine("You used " + currentItem.Name);
                                inventory.RemoveItem(currentItem);
                                itemUsed = true;
                                Console.WriteLine();
                            }
                        }
                        break;

                    default:
                        Console.WriteLine("Command not recognized, try again.");
                        Console.WriteLine();
                        break;
                }
            }

            if (currentItem.Damage >= currentEvent.RequiredStats.Damage || currentItem.Block >= currentEvent.RequiredStats.Block || currentItem.Magic >= currentEvent.RequiredStats.Magic)
            {
                Console.WriteLine(currentEvent.WinText);
                Item[] rewards = currentEvent.GetRewardItems();
                for (int i = 0; i < rewards.Count(); i++)
                {
                    Console.WriteLine("Obtained " + rewards[i].Name + "!");
                    inventory.AddItem(rewards[i]);
                }
                Console.WriteLine();
                lvlComplete++;
            }
            else
            {
                lose = true;
            }

        }

        Console.WriteLine("GAME OVER" + "\nEncounters Completed: " + lvlComplete);
    }
}
