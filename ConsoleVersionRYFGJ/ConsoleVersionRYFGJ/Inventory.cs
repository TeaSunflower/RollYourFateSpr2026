using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVersionRYFGJ
{
    internal class Inventory
    {
        // Fields
        private List<Item> itemStorage;
        private int storageCount;

        /// <summary>
        /// Default Constructor
        /// </summary>
        public Inventory()
        {
            itemStorage = new List<Item>();
            storageCount = 0;
        }

        public Inventory(List<Item> starterSet)
        {
            itemStorage = new List<Item>();
            for(int i = 0; i < starterSet.Count; i++)
            {
                AddItem(starterSet[i]);
            }
        }

        /// <summary>
        /// Adds an item to the inventory
        /// </summary>
        /// <param name="item"> Item to be added </param>
        public void AddItem(Item item)
        {
            itemStorage.Add(item);
            storageCount++;
        }

        /// <summary>
        /// Seaches the inventory for the first instance of an item
        /// </summary>
        /// <param name="item"> Item to be found </param>
        public Item SearchItem(string itemName)
        {
            for(int i = 0; i < storageCount; i++)
            {
                if (itemName.ToLower() == itemStorage[i].Name.ToLower())
                {
                    return itemStorage[i];
                }
            }

            return new Item("null", 0, 0, 0);
        }

        /// <summary>
        /// Removes the first instance of an item
        /// </summary>
        /// <param name="item"> Item to be removed </param>
        public void RemoveItem(Item item)
        {
            itemStorage.Remove(item);
            storageCount--;
        }

        public void PrintItemList()
        {
            Console.WriteLine("Inventory:");
            for (int i = 0; i < storageCount; i++)
            {
                Console.WriteLine(itemStorage[i].Name);
            }
        }
    }
}
