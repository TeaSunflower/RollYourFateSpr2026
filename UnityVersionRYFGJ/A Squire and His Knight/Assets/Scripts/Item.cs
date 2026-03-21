using UnityEngine;

public struct Item
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
