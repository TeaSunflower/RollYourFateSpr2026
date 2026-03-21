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
        private List<Item> itemStorage = new List<Item>();
        private int storageCount = 0;

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
        public Item SearchItem(Item item)
        {
            for(int i = 0; i < storageCount; i++)
            {
                if (item.Name == itemStorage[i].Name)
                {
                    return itemStorage[i];
                }
            }

            return itemStorage[0];
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
    }
}
