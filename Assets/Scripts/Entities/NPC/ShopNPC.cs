using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopNPC : NPCInteractor // extend the base NPCInteractor script so we can override the OnInteract method so it executes this code instead.
{
    private GameObject shopUI; // get the shop UI, so different shop NPCs can have different UIs.
    public string shopName; // get the shop name, so different shop NPCs can have different UIs.

    private void Start()
    {
   

        foreach(GameObject shop in ObjectsHandler.instance.shopUI) // loop all the existing shop UIs
        {
            if(shop.name.Equals(shopName + "Shop")) // see which shop this npc is. if it corresponds, 
            {
                shopUI = shop;  // save the current iterated shop into the shopUI variable.
                break; // then break out of the loop as we don't need to find anymore.
            }
        }
    }
    public override void OnInteract() // override the OnInteract method
    {
        base.OnInteract(); 

        shopUI.SetActive(true); // to set the shop UI active  when interacting
    }

    public override void OnUnInteract() // when un interact,
    {
        base.OnUnInteract();

        shopUI.SetActive(false); // turn off the shop UI.
    }
}
