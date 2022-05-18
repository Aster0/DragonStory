using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemMouseEvents : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler

    // attached to the inventory, equipment and shop menu to check for mouse events.
{

    public GameObject statsInformationUI, rightClickUI;

    private ItemSlotManager itemSlotManager;
    private Outline panelOutline;


    public bool rightClickEvent;

    private Item item;

    private void Start()
    {
  

        if (rightClickEvent) // if it's a UI with a right click event,
        {
            itemSlotManager = GetComponent<ItemSlotManager>(); // we will get the itemSLotManager to look into what's in that slot
            panelOutline = itemSlotManager.panel.GetComponent<Outline>(); // get the panelOutline so we can right click 

            


        }
        else // if not, 
        {
           
            item = GetComponent<ShopItemDescription>().item; // just get the shop item description's item so we can reflect on the info later
        }
    }


    //Detect if a click occurs
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
       

        if(rightClickEvent) // if it's with a right click (inv and equopment)
            item = itemSlotManager.item; // update the item again in the slot

      
        if (item != null) // if there's an item in the slot
        {
            statsInformationUI.SetActive(true); // set the stat information UI to display extra info on

            string stats;

            if (rightClickEvent) // if it's a ui with right click
            {
                panelOutline.enabled = true; // for the right click UI to be enabled

                stats = "+" + item.adder + " "+ item.stats.ToString(); // update how much stats the item gives 
            }
            else
            {
                stats = "You get a mystery stat upon purchasing!"; // prompt the user that they get a mstery stat upon purchasing,
                // it's different from the rightClickEvent's text because this is before the player buys, so no stat is assigned yet
                // as it's being viewed in the shop.
            }

          

            statsInformationUI.GetComponent<ItemInformation>()
                .SetInformation(item.name, item.description,  stats); // update the extra stat info's UI with the information gathered
        }
    }

    public void OnPointerExit(PointerEventData eventData) // when the cursor leaves 
    {
        statsInformationUI.SetActive(false); // off the extra info UI
         
        if(rightClickEvent) // if it's with a right click ui
        {
            panelOutline.enabled = false; // set the right click panel Outlne to false, means it's closed.
            rightClickUI.SetActive(false);  // turn off the right click UI
        } // this is so, if u right clicked an item and un-hovered it, it'll auto close all extra menus in the inv/equipment ui.

    }

    public void OnPointerClick(PointerEventData eventData) // if we click on the item
    {
        if(rightClickEvent) // if it's with a right click event
        {
            if (itemSlotManager.item != null) // if there's an item in the slot
            {
                if (eventData.button == PointerEventData.InputButton.Right) // if it's right click not left click
                {
                    rightClickUI.SetActive(true); // set the right click menu to appear.
                }
            }
        }
     
    }
}


