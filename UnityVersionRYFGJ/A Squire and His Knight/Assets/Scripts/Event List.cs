using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EventList
{
    private List<Event> events;

    public EventList()
    {
        events = new List<Event>();
        GetEvents();
    }

    /// <summary>
    /// Uses FileIO to obtain all events from a file 
    /// </summary>
    private void GetEvents()
    {
        StreamReader reader = new StreamReader("Assets/Files/Events.txt");

        try
        {
            do
            {
                string eventInfo = reader.ReadLine()!;

                if (eventInfo[0] != '#') // # can be used to represent comments in the Events file
                {
                    // Split into list

                    List<string> parsedEvent = new List<string>();

                    int indexOfBreakCharacter = 0;
                    while (indexOfBreakCharacter != -1)
                    {
                        // Obtain next index
                        indexOfBreakCharacter = eventInfo.IndexOf('|');

                        // Put information into string list
                        if (indexOfBreakCharacter != -1)
                        {
                            parsedEvent.Add(eventInfo.Substring(0, indexOfBreakCharacter));
                            // Delete from original string
                            eventInfo = eventInfo.Substring(indexOfBreakCharacter + 1, eventInfo.Length - indexOfBreakCharacter - 1);
                        }
                        // If last info from the string
                        else
                        {
                            parsedEvent.Add(eventInfo);
                        }
                    }

                    // Get Info from list
                    // Add new Event
                    Event newEvent = new Event(int.Parse(parsedEvent[2]), int.Parse(parsedEvent[3]), int.Parse(parsedEvent[4]), parsedEvent[0], parsedEvent[1]);

                    // Add rewards to event
                    for (int i = 5; i < parsedEvent.Count; i += 4)
                    {
                        newEvent.AddToReward(new Item(parsedEvent[i], int.Parse(parsedEvent[i + 1]), int.Parse(parsedEvent[i + 2]), int.Parse(parsedEvent[i + 3])));
                    }
                    events.Add(newEvent);
                }
            }
            while (reader.Peek() != -1);
        }
        catch
        {
            Debug.Log("Error: Failed to read file");
        }
        finally
        {
            reader.Close();
        }
    }

    // Obtain a random Event from the event pool
    public Event GetRandomEvent()
    {
        return events[UnityEngine.Random.Range(0, events.Count)];
    }
}
