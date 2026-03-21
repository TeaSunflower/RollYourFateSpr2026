using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVersionRYFGJ
{
    /// <summary>
    /// Item is a container class for item objects. It has 3 stats that will be used in encounters. 
    /// </summary>
    internal struct Item
    {
        private string name;
        private int damage;
        private int block;
        private int magic;

        public string Name
        {
            get { return name; }
        }

        public int Damage
        {
            get { return damage; }
        }
        public int Block
        {
            get { return block; }
        }
        public int Magic
        {
            get { return magic; }
        }

        public Item(string _name, int _damage, int _block, int _magic)
        {
            name = _name;
            damage = _damage;
            block = _block;
            magic = _magic;
        }

        public Item(int _damage, int _block, int _magic)
        {
            name = "No Name";
            damage = _damage;
            block = _block;
            magic = _magic;
        }
    }
}
