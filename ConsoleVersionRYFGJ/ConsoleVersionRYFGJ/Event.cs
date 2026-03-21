using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVersionRYFGJ
{
    internal struct Event
    {
        private Item requiredStats;
        private Item rewardItem;

        public Item RequiredStats
        {
            get { return requiredStats; }
        }

        public Item RewardItem
        {
            get { return rewardItem; }
        }

        public Event(int _requiredDamage, int _requiredBlock, int _requiredMagic, string _rewardItemName, int _rewardItemDamage, int _rewardItemBlock, int _rewardItemMagic)
        {
            requiredStats = new Item(_requiredDamage, _requiredBlock, _requiredMagic);
            rewardItem = new Item(_rewardItemName, _rewardItemDamage, _rewardItemBlock, _rewardItemBlock);
        }
    }
}
