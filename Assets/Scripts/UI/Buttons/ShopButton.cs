using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour // base system for how the shop buttons work.
{

    private Button button;

    public TextMeshProUGUI goldText, itemNameText;
    public Image itemImage;


    public int attackSpeedRarity, damageRarity, healthRarity, knockbackRarity;

    // Start is called before the first frame update
    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OnButtonClick); // add the button on click listener on the OnButtonClick method.
    }

    
    private void OnButtonClick() // when the button is clicked, the player is trying to purchase. 
    {


        int price = int.Parse(goldText.text);

        if (!(PlayerEntity.player.coins >= price)) // player does not have coins above or equal the price.  ( means no money )
        {
            ObjectsHandler.instance.popMenu.SetMessage("Not enough coins!");
            return;
        }

  

        ShopItemDescription shopItemDescription = gameObject.GetComponent<ShopItemDescription>(); // get the shop item description which
        // hold what item the shop is selling.

        Item shopItem;

        if (shopItemDescription != null) // if this is not null, means there's an item to buy to put in your inventory.
        {
            shopItem = shopItemDescription.item; // get the shop item that is placed in the shopItemDescription script

            string itemName = itemNameText.text;
        
            Sprite itemSprite = itemImage.sprite;

            if (shopItem.type == ItemType.Equipment) // if it's an equipment,
            {

                List<ItemStats> itemStats = new List<ItemStats>(); // list containing all possible item stats the equipment can get.


                /*
                 * 
                 *  how the rarity works is
                 *  
                 *  if for example, for attackSpeed, it has a 10% rarity. meaning, the for loop will iterate 10 times, to add it into the arraylist.
                 *  this means that the attackSpeedRarity has 10 entries, and 10 out of the total other stats that will be added into the arraylist.
                 *  if all the stats add up to 100, attackspeed will have 10%.
                 * 
                 */ 
                for (int i = 0; i <= attackSpeedRarity; i++) 
                {
                    itemStats.Add(ItemStats.Attackspeed);
                }
                for (int i = 0; i <= healthRarity; i++)
                {
                    itemStats.Add(ItemStats.Health);
                }
                for (int i = 0; i <= damageRarity; i++)
                {
                    itemStats.Add(ItemStats.Damage);
                }
                for (int i = 0; i <= knockbackRarity; i++)
                {
                    itemStats.Add(ItemStats.Knockback);
                }


                ItemStats stat = itemStats[Random.Range(0, itemStats.Count)]; // randomize the stats gotten


                int divide = 1; // if it's not knockback or attackspeed, divide by 1, meaning the stats will have no change.

                if (stat == ItemStats.Knockback) // if it's a knockback random stat 
                {
                    divide = 10; // divide the stats by 10 so it's not too strong.
                }
                else if (stat == ItemStats.Attackspeed) // if it's an attackspeed random stat 
                {
                    divide = 100; // divide the stats by 10 so it's not too strong.
                }

                shopItem.adder = ((int)PlayerEntity.player.level + Random.Range(0, 10)) / divide; // set the random stats adder 

                shopItem.stats = stat; // set the random stat to the stat that we randomized just now.

                Item item = (Item)ScriptableObject.CreateInstance("Item"); // create the scriptableobject instance
                item.name = shopItem.name; // assign the item with the current item that the shop's selling's name
                item.slot = shopItem.slot; // assign the item with the current item that the shop's selling's slot
                item.sprite = shopItem.sprite; // assign the item with the current item that the shop's selling's sprite
                item.stats = shopItem.stats; // assign the item with the current item that the random stat generated just now
                item.adder = shopItem.adder; // assign the item with the current item that the shop's selling's name
                item.description = shopItem.description; // assign the item with the current item that the shop's selling's description
                item.type = shopItem.type; // assign the item with the current item that the shop's selling's type


                //Item item = new Item(shopItem.name, shopItem.slot, shopItem.sprite, shopItem.stats,
                //shopItem.adder, shopItem.description, shopItem.type); // create a new instance of the item so it doesn't edit the shop's item and we can save uniquely to the inventory.

                bool added = ObjectsHandler.instance.inventoryManager.OnAddItem(item);


                if (!added) // if it's not added, 
                {
                    ObjectsHandler.instance.popMenu.SetMessage("Inventory Full!"); // pop an error message
                    return; // and return, do not continue down.
                }
            }
        }
        else // else if it's null, there's nothing to put in the inventory, so it should be a potion. so add to the total potion instead
        {
         

            PotionQuantity potionQuantity = ObjectsHandler.instance.player.GetComponent<PlayerInteract1>().potionQuantity; // get the potionQuantity instance from the player instance saved in the ObjectsHandler singleton by getting the PlayerInteract1 script.

            potionQuantity.SetQuantity(potionQuantity.GetQuantity() + 1); // set the quantity of the potions as the start quantity.

      
        }

        










            // pay, remove coins from player.
            PlayerEntity.player.coins -= price;
            ObjectsHandler.instance.goldUpdate.SetGold(PlayerEntity.player.coins);
            ObjectsHandler.instance.popMenu.SetMessage("Item Purchased"); // success message
    }
}
