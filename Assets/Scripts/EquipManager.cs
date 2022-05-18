using UnityEngine;

public class EquipManager : MonoBehaviour
{

    private GameObject[] slots; // keeps all the slots in an array so even when the equipment canvas is setactive to false, we have the previous saved instances.

    private void Start()
    {
        slots = new GameObject[GameObject.FindGameObjectsWithTag("EquipmentSlot").Length]; // creates an array with the length of the total slots,
        // the total length is found by finding every game object with the EquipmentSlot tag.



        for (int i = 1; i <= slots.Length; i++) // iterate from 1 to the amount of slots
        {
            slots[i - 1] = GameObject.Find("EquipmentSlot" + i); // save it in the slots variable.
        }


    }



    public void equipToSlot(Item item) // function to call when I want to equip an item to slot (the parameter fetches the item I want to equip)
    {
        setEquipment(item); // call the setEquipment function with item as its parameter
    }
    public void unequipFromSlot(Item item, int slot) // function to call when I want to unequip an item from the slot (the parameter fetches the item I want to unequip and the slot - slot determines what equipment it is, e.g. slot 3 is the bracers)
    {
        setEquipment(item, slot);   // call the setEquipment function with item as its parameter
        // setEquipment is an overloaded function, differentiated by the parameter. 
    }

    private void setEquipment(Item item, int removeSlot) // removeSlot determines the slot to remove in the equipment.
    {


        int adder = -(item.adder); // negate so it minuses when uneqip.

        bool equipVisual = false; // unequip the visuals

        int slot = removeSlot; // creates a local variable that references the parameter's removeSlot into this variable.



        PlayerEntity.player.equipment[slot] = null; // set to null, no items in this slot.


        StatsUpdater statsUpdater = ObjectsHandler.instance.statsUI.GetComponent<StatsUpdater>();
        // get the statsupdater script from the statUI from the object handler singleton.





        if (item.stats == ItemStats.Damage) // check if the item's stat is damage
        {
            statsUpdater.AddDamageAmount(adder); // if yes, we will add the adder into the damage. at the start, it's negated because this function is for removing of equipments, so minus off the stats when unequip
        }
        else if (item.stats == ItemStats.Health) // check if the item's stat is health
        {
            statsUpdater.AddHealthAmount(adder); // if yes, we will add the adder into the health. at the start, it's negated because this function is for removing of equipments, so minus off the stats when unequip
        }
        else if (item.stats == ItemStats.Knockback) // check if the item's stat is knockback
        {
            PlayerEntity.player.knockback += adder; // if yes, we will add the adder into the knockback. at the start, it's negated because this function is for removing of equipments, so minus off the stats when unequip
        } 
        else if (item.stats == ItemStats.Attackspeed) // check if the item's stat is attackspeed
        {
            PlayerEntity.player.attackSpeed += adder; // if yes, we will add the adder into the attackspeed. at the start, it's negated because this function is for removing of equipments, so minus off the stats when unequip
        }

        if (item.slot == ItemSlot.Head) // check if the item slot is a head equipment
        {
            ObjectsHandler.instance.equipmentHelmet.SetActive(equipVisual); // remove the visual (this is unequipping)
        }
        else if (item.slot == ItemSlot.Weapon) // check if the item slot is a weapon equipment
        {
            ObjectsHandler.instance.equipmentSword.SetActive(equipVisual); // remove the visual (this is unequipping)
        }
        else if (item.slot == ItemSlot.Bracer) // check if the item slot is a bracers equipment
        {

            for (int i = 0; i < 2; i++) // loop becaus there's two bracers
            {
                ObjectsHandler.instance.equipmentBracers[i].SetActive(equipVisual); // remove the visual (this is unequipping)
            }

        }


        ItemSlotManager itemSlotManager = slots[(int)item.slot - 1].GetComponent<ItemSlotManager>(); // get the ItemSlotManager script by 
        // accessing the slots array that we modified at the start. 
        itemSlotManager.item = null; // set the ItemSlotManager instance's item to null because it's unequipping

        itemSlotManager.OnSlotChange(); // call the onslotchange function so that the UI updates accordingly.
    }
    private void setEquipment(Item item)
    {
        PlayerEntity.player.equipment[(int) item.slot - 1] = item; // convert the ItemSlot enum to int so we can get the id. then save to that index of the array.


        StatsUpdater statsUpdater = ObjectsHandler.instance.statsUI.GetComponent<StatsUpdater>();


        if (item.stats == ItemStats.Damage) // check if the item's stat is damage
        {
            statsUpdater.AddDamageAmount(item.adder); // if yes, we will add the adder into the damage. 
        }
        else if (item.stats == ItemStats.Health) // check if the item's stat is health
        {
            statsUpdater.AddHealthAmount(item.adder); // if yes, we will add the adder into the health. 
        }
        else if (item.stats == ItemStats.Knockback) // check if the item's stat is knockback
        {
            PlayerEntity.player.knockback += item.adder; // if yes, we will add the adder into the knockback.
        }
        else if (item.stats == ItemStats.Attackspeed) // check if the item's stat is attackspeed
        {
            PlayerEntity.player.attackSpeed += item.adder; // if yes, we will add the adder into the attackspeed. 
        }

        if (item.slot == ItemSlot.Head) // check if the item slot is a head equipment
        {
            ObjectsHandler.instance.equipmentHelmet.SetActive(true); // show the visual (this is unequipping)
        }
        else if (item.slot == ItemSlot.Weapon) // check if the item slot is a weapon equipment
        {
            ObjectsHandler.instance.equipmentSword.SetActive(true); // show the visual (this is unequipping)
        }
        else if (item.slot == ItemSlot.Bracer) // check if the item slot is a bracers equipment
        {

            for (int i = 0; i < 2; i++) // loop becaus there's two bracers
            {
                ObjectsHandler.instance.equipmentBracers[i].SetActive(true); // show the visual (this is unequipping)
            }

        }


        ItemSlotManager itemSlotManager = slots[(int)item.slot - 1].GetComponent<ItemSlotManager>(); // get the ItemSlotManager script by 
        // accessing the slots array that we modified at the start. 
        itemSlotManager.item = item; // update the item to the equipped item in ItemSlotManager's instance.

        itemSlotManager.OnSlotChange();  // call the onslotchange function so that the UI updates accordingly.
    }

}
