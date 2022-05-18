using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Item", menuName = "Item")] // so we can create an item by right clicking
public class Item  : ScriptableObject // scriptable object to save large data of a certain item
{

    public Item(string name, ItemSlot slot, Sprite sprite, ItemStats stats, int adder, string description, ItemType type) // Item Constructor, with some parameters to assign to our Item Instance's global variables.
    {

        // this refers to this instance, so assign this.name (name global variable) to the parameter's name variable.
        this.name = name; 
        this.slot = slot;
        this.sprite = sprite;
        this.stats = stats;
        this.adder = adder;
        this.description = description;
        this.type = type;
    }


    public new string name;
    public string description;
    public ItemSlot slot;
    public ItemType type;

    public Sprite sprite;// item sprite.


    public ItemStats stats;

    public int adder; // add how many stats?




}



public enum ItemSlot // enum to determine item slot
{
    None,
    Head = 1, // give the equipment slots id, head is 1, weapon is 2, ring is 3
    Weapon = 2,
    Bracer = 3
}

public enum ItemStats // enum to determine item stats
{
    Health,
    Damage,
    Knockback,
    Attackspeed

}

public enum ItemType // enum to determine item type
{
    Equipment,
    Consumable,

}