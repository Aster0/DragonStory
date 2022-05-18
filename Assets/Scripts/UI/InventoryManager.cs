using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    private int inventorySize;


    [HideInInspector]
    public GameObject[] slots; // keeps all the slots in an array so even when the equipment canvas is setactive to false, we have the previous saved instances.






    // Start is called before the first frame update
    void Awake()
    {

        inventorySize = GameObject.FindGameObjectsWithTag("InventorySlot").Length; // dynamic, so I can add more slots later easily.
                                                                                   // the total length is found by finding every game object with the InventorySlot tag.




    }

    private void Start()
    {
        slots = new GameObject[inventorySize]; // creates an array with the length of the total slots,
                                               




        for (int i = 1; i <= inventorySize; i++) // iterate from 1 to the amount of inventory slots
        {
            slots[i - 1] = GameObject.Find("Slot" + i); // save it in the slots variable.
        }

     
    }



    public bool OnAddItem(Item item) // when adding items to the inventory
    {


  
        int addedSlot = PlayerEntity.player.inventory.Count; // get the slot that will be added, because Count = +1 of the current slot, we essentially get what is the slot that will be added later.

   
        
        if(addedSlot >= inventorySize) // if the next added slot is already above or equal than the inventory size, means we can't add.
        {

       
            return false; // so return false to say we can't add to the inventory.
        }
   

        PlayerEntity.player.inventory.Insert(addedSlot, item); // if no returns above, means we can add, inventory is not full.


        GameObject slot = slots[addedSlot]; // get the GameObject slot from the slots array with the addedSlot index.
    

        ModifySlot(slot, item); // modify that slot, with the item that is put in the function's parameter


        return true; // return true for success




    }

    public bool OnRemoveItem(int removeSlot) // on item remove from inventory
    {


        if (removeSlot < 1) // checks if the removeSlot is a valid slot (Slot1, Slot2, etc) not Slot0.
        {
            return false; // if it's not a valid slot (Slot0), return false. means remove not successful.
        }


        

        PlayerEntity.player.inventory.RemoveAt(removeSlot - 1); // remove the item from the player's inventory list at removeslot - 1 index.
     


        GameObject slot = slots[removeSlot - 1]; // get the GameObject slot from the slots array with the removeSlot - 1 index.

    

        ModifySlot(slot, null); // modify the slot to item null, meaning that slot should have nothing, thus, removing the item.

        for (int i = removeSlot; i <= inventorySize; i++) // update the inventory from the slot that was removed, to bring everything back down in the inventory UI visual.
        {

            slot = GameObject.Find("Slot" + i); // assign slot to the current iteration of the for loop. if first iteration, we'll be finding the Slot1 Gameobject.

            try
            {
                ModifySlot(slot, PlayerEntity.player.inventory[i - 1]); // modify that slot with what's inside the inventory array with index i - 1
                // reason because, we removed the index just now using RemoveAt(removeSlot - 1), so in the array, everything is one down starting from
                // the removeSlot, so we should visually update the inventory too.
            }
            catch (Exception)
            { 
                ModifySlot(slot, null); // if exception is called, means nothing is in that index in the Player Inventory List. So make it null to ensure the inventory visualizes nothing.
            }
            
        }
   




        return true; // return true for success




    }

    private void ModifySlot(GameObject slot, Item item) // function to call when modifying the inv slot. 
    {
        ItemSlotManager itemSlotManager = slot.GetComponent<ItemSlotManager>(); // get the iItemSlotManager script from the GameObject slot that is supplied in the parameter
        itemSlotManager.item = item; // modify the slot's item to the item in t he parameter.
        itemSlotManager.OnSlotChange(); // call the on slot change listener.


    }

    public void UpdateWholeInventory() // update the visual for everything
    {

        for (int i = 0; i < inventorySize; i++) // update the inventory from the slot that was removed, to bring everything back down in the inventory UI visual.
        {
            GameObject slot = slots[i]; // same concept as above but instead of starting from removeSlot, we start from 0, essentially
            // updating the whole inv.

            try
            {
                ModifySlot(slot, PlayerEntity.player.inventory[i]); // same concept as above but instead of starting from removeSlot, we start from 0, essentially
                                                                    // updating the whole inv.
            }
            catch (Exception)
            {
                ModifySlot(slot, null); // if exception is called, means nothing is in that index in the Player Inventory List. So make it null to ensure the inventory visualizes nothing.
            }

        }
    }
}
