using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleVersionRYFGJ
{
    internal struct Item
    {
        private int damage;
        private int block;
        private int magic;

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

        public Item(int _damage, int _block, int _magic)
        {
            damage = _damage;
            block = _block;
            magic = _magic;
        }
    }
}
