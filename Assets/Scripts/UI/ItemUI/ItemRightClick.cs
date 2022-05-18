using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemRightClick : MonoBehaviour // for the right click menu, how it behaves 
{
    // Start is called before the first frame update


    public Button equipButton, dropButton;

    void Start()
    {
        equipButton.onClick.AddListener(OnEquip); // add the listener for the equipbutton so it knows what to do when clicked
        dropButton.onClick.AddListener(OnDrop); // add the listener for the dropbutton so it knows what to do when clicked


    }


    private Item GetItemInSlot(int slot, bool equipment) // get the item in the current slot
    {
        Item itemInSlot;

        if (!equipment) // if it's not in equipment
            itemInSlot = PlayerEntity.player.inventory[slot - 1]; // get the item in slot in the inventory
        else // if it's an equipped item
            itemInSlot = PlayerEntity.player.equipment[slot - 1]; // get the item in slot in the equipment ui

        return itemInSlot; // return the item in slot found
    }

    private void OnEquip() // when equipping an item,
    {
  

        int slot = int.Parse(transform.parent.name.Replace("Slot", "")); // replace the Slot into empty so we can get the number behind. The game object name is something like Slot1.

        Item itemInSlot = GetItemInSlot(slot, false); // get the item in the specified slot in the parameter, false because it's not equipped yet.


        if (itemInSlot.type == ItemType.Equipment && PlayerEntity.player.equipment[(int) itemInSlot.slot - 1] == null) // check if the item is an equipment so we can equip it. and if there's nothing in that slot at the moment
        {

            
            ObjectsHandler.instance.equipManager.equipToSlot(itemInSlot); // equip to the equipment slot


            ObjectsHandler.instance.inventoryManager.OnRemoveItem(slot); // remove the item from the inventory slot
  
        }
        else // if there are equipped item in that slot already
        {
            ObjectsHandler.instance.popMenu.SetMessage("You can't equip the " + itemInSlot.name + "!", 1); // give an error message
        }


        OnFinish(); // call the on finish method
    }

    private void OnDrop() // on item drop
    {


        int slot; 

   

        if (!PlayerEntity.player.inEquipment) // in inventory
        {
            slot = int.Parse(transform.parent.name.Replace("Slot", "")); // replace the Slot into empty so we can get the number behind. The game object name is something like Slot1.

            ObjectsHandler.instance.inventoryManager.OnRemoveItem(slot); // remove the item from the inventory with the specified slot
        }
        else // in equipment
        {
           

            slot = int.Parse(transform.parent.name.Replace("EquipmentSlot", "")); // replace the EquipmentSlot into empty so we can get the number behind. The game object name is something like EquipmentSlot1.
        
            
            Item itemInSlot = GetItemInSlot(slot, true); // get the item in the slot, true because it's equipped as player is in equipment
            // as per the if clauses

     

            bool added = ObjectsHandler.instance.inventoryManager.OnAddItem(itemInSlot); // for the equipped item, instead of dropping,
            // it's put back in the inventory (unequipping) so this puts back into the inventory. the returned boolean determines if it's
            // successful or not. fail = false, sucess = true


            if (!added) // if fail to add back into the inventory,
            {
                ObjectsHandler.instance.popMenu.SetMessage("Inventory Full!"); // send error message to player
                return; // return, dont go down any furhter
            }

            ObjectsHandler.instance.equipManager.unequipFromSlot(itemInSlot, slot - 1); // equip to the equipment slot

           
        }


        OnFinish(); // call the on finish method.
    }

    private void OnFinish()
    {
        gameObject.SetActive(false); // on finish just sets the current game object off. this is attached to the right click menu,

        // so the right click menu will be closed after clicking on a button.
    }
}
