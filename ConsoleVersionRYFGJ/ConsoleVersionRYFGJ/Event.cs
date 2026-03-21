using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVersionRYFGJ
{
    internal class Event
    {
        private string description;
        private string winText;
        private Item requiredStats;
        private List<Item> rewardItems;

        public string Description
        {
            get { return description; }
        }

        public string WinText
        {
            get { return winText; }
        }

        public Item RequiredStats
        {
            get { return requiredStats; }
        }

        public Event(int _requiredDamage, int _requiredBlock, int _requiredMagic, string _description, string _winText)
        {
            requiredStats = new Item(_requiredDamage, _requiredBlock, _requiredMagic);
            rewardItems = new List<Item>();
            description = _description;
            winText = _winText;
        }

        /// <summary>
        /// Adds reward to the reward pool of the Event
        /// </summary>
        /// <param name="_rewardItem">Item being added to the event pool</param>
        public void AddToReward(Item _rewardItem)
        {
            rewardItems.Add(_rewardItem);
        }

        /// <summary>
        /// Obtains the reward Items in an array
        /// </summary>
        /// <returns>Array of the reward Items</returns>
        public Item[] GetRewardItems()
        {
            Item[] itemList = new Item[rewardItems.Count];
            for(int i = 0; i < itemList.Length; i++)
            {
                itemList[i] = rewardItems[i];
            }

            return itemList;
        }
    }
}
