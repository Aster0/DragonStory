using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetup : MonoBehaviour
{
    /*
 * 
 * Class to setup the player in the scene editor to set damage, health, etc.
 */

    public HealthBarControl healthBarControl;
    public EXPBar expBar;
    public GameObject skin;


    [Header("Player Start Stats")]
    public int baseDamage = 1;

    public int baseHealth = 20;
    public float baseAttackSpeed = 1;
    public float knockback1 = 40;
    public int jumpHeight = 250;

    public long startLevel = 1;

    public int startPotions = 10;

    private int startStatsPoints;


 

    
    // Start is called before the first frame update
    void Start()
    {
        PlayerEntity.player.health = baseHealth; // access the player
        healthBarControl.SetMaxHealth(PlayerEntity.player.health); // set max health.
        healthBarControl.Heal(PlayerEntity.player.health); // heal up to max health.

        PlayerEntity.player.attackSpeed = baseAttackSpeed;
        PlayerEntity.player.damage1 = baseDamage;
        PlayerEntity.player.knockback = knockback1;
        PlayerEntity.player.level = startLevel;
        PlayerEntity.player.jumpHeight = jumpHeight;
        PlayerEntity.player.statPoints = 0;


        PotionQuantity potionQuantity = ObjectsHandler.instance.player.GetComponent<PlayerInteract1>().potionQuantity; // get the potionQuantity instance from the player instance saved in the ObjectsHandler singleton by getting the PlayerInteract1 script.

        potionQuantity.SetQuantity(startPotions); // set the quantity of the potions as the start quantity.

      

        expBar.SetMaxEXP(PlayerEntity.player.level * 100); // update the starting max exp if the player doesn't start at lvl 1, we can set it.
        expBar.SetEXP(0); // reset the current exp



        PlayerEntity.player.inventory = new List<Item>(); // assign a brand new empty list to the inventory variable of PlayerEntity.


        PlayerEntity.player.equipment = new Item[3]; // array of item with 3 slots in total, 0, 1, 2.





    }




}
